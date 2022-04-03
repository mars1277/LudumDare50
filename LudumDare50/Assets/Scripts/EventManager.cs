using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    [Header("Static Variables")]
    public static int roundCounter = 0;
    public static float points;
    public static List<Sprite> actualCards;
    public static int actualBeastFaceId;

    [Header("Difficulty Settings")]
    public float baseRoundTime = 10.0f;
    public float roundTimeReductionPercent;
    public int increaseDifficultyPerXRound;
    public int randomizeCardsAtDifficulty = -1;

    [Header("Point Calculation Settings")]
    public int baseRoundPoints;
    public int minPointsAt0Secs;
    public int difficultyAdditivePercentageBonus;
    public int timeReductionMultiplicativePercentageBonus;
    [Header("Sound Settings")]
    public float minMonsterSoundEffectSecs;
    public float maxMonsterSoundEffectSecs;
    [Header("Code Variables")]
    public float roundTime = 10.0f;
    public bool roundEnded = true;
    public int cardsDifficulty;
    public float actualMonsterSoundEffectSecs;
    public float monsterSoundEffectTimer;
    public int reduceTimeAtThisStage = 1;
    public int timeReductionCounter = 0;
    [Header("GameObjects")]
    public TMP_Text roundCountText;
    public Slider roundTimerBar;
    public TMP_Text pointsText;
    public Image beastFaceImage;
    public GameObject cardsGroup;
    [Header("Prefabs")]
    public GameObject cardGO;
    public GameObject soundEffectManagerGO;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("SoundEffectManager") == null)
        {
            GameObject soundEffectManager = Instantiate(soundEffectManagerGO);
            soundEffectManager.name = soundEffectManager.name.Substring(0, soundEffectManager.name.Length - 7);
        }
        actualMonsterSoundEffectSecs = Random.Range(minMonsterSoundEffectSecs, maxMonsterSoundEffectSecs);
        monsterSoundEffectTimer = 0;
        if (SceneManager.GetActiveScene().name == "Game")
        {
            roundEnded = true;
            points = 0;
            pointsText.text = "" + points;
            if (randomizeCardsAtDifficulty == -1)
            {
                randomizeCardsAtDifficulty = GameObject.Find("CardSets").GetComponent<CardSets>().cardSetGroups.cardSetGroups.Count;
            }
            StartFirstRound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!roundEnded)
        {
            roundTime -= Time.deltaTime;
            if (roundTime < 0)
            {
                EndLostRound();
            } else
            {
                roundTimerBar.value = roundTime;
            }
            monsterSoundEffectTimer += Time.deltaTime;
            if (monsterSoundEffectTimer >= actualMonsterSoundEffectSecs)
            {
                GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().PlayRandomMonsterSound();
                actualMonsterSoundEffectSecs = Random.Range(minMonsterSoundEffectSecs, maxMonsterSoundEffectSecs);
                monsterSoundEffectTimer = 0;
            }
        }

    }

    public void StartFirstRound()
    {
        //set round count
        roundCounter = 1;
        roundCountText.text = "" + roundCounter;
        //create round
        CreateRound();
        //start round
        roundTimerBar.maxValue = baseRoundTime;
        roundTime = baseRoundTime;
        roundTimerBar.value = roundTime;
        roundEnded = false;
    }

    public void StartNewRound()
    {
        //set round count
        roundCounter++;
        roundCountText.text = "" + roundCounter;
        //create round
        CreateRound();
        //start round
        roundTimerBar.maxValue = baseRoundTime;
        roundTime = baseRoundTime;
        roundTimerBar.value = roundTime;
        roundEnded = false;
    }


    public void CreateRound()
    {
        //delete previous cards
        foreach (Transform child in cardsGroup.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //get card sets based on round
        int newCardsDifficulty = CalculateCardsDifficulty();
        //choose a card set from the card sets
        CardSets.CardSet cardSet = GameObject.Find("CardSets").GetComponent<CardSets>().GetRandomCardSetInRandomizedOrder(newCardsDifficulty);
        actualCards = cardSet.cards;
        //choose a card to be the face of the beast
        int beastFaceId = cardSet.GetBeastFaceId();
        actualBeastFaceId = beastFaceId;
        //create and show cards in the game, one with win condition, one with lose one
        for (int i = 0; i < cardSet.cards.Count; i++)
        {
            GameObject card = Instantiate(cardGO, cardsGroup.transform);
            card.transform.GetChild(0).GetComponent<Image>().sprite = cardSet.cards[i];
            if (i == beastFaceId)
            {
                beastFaceImage.sprite = cardSet.cards[i];
                card.GetComponent<Button>().onClick.AddListener(EndSuccessfulRound);
            } else
            {
                card.GetComponent<Button>().onClick.AddListener(EndLostRound);
            }
        }
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().PlayCardPullingSound();
    }

    public int CalculateCardsDifficulty()
    {
        bool randomizeDifficulty = cardsDifficulty >= randomizeCardsAtDifficulty;
        int newCardsDifficulty = 0;
        if (randomizeDifficulty) {
            newCardsDifficulty = Random.Range(0, randomizeCardsAtDifficulty);
        } else
        {
            if (roundCounter > 1 && (roundCounter - 1) % increaseDifficultyPerXRound == 0)
            {
                cardsDifficulty++;
            }
            newCardsDifficulty = cardsDifficulty;
        }
        return newCardsDifficulty;
    }

    public void EndSuccessfulRound()
    {
        if (!roundEnded)
        {
            float p = CalculatePoints();
            points += p;
            pointsText.text = "" + points;
            CalculateNewBaseRoundTime();
            StartNewRound();
        }
    }

    public void CalculateNewBaseRoundTime()
    {

        timeReductionCounter++;
        if (timeReductionCounter == reduceTimeAtThisStage)
        {
            reduceTimeAtThisStage++;
            timeReductionCounter = 0;
            baseRoundTime = baseRoundTime * ((100 - roundTimeReductionPercent) / 100f);
        }
    }
    /*
     
    public int difficultyAdditivePercentageBonus;
    public int timeReductionAdditivePercentageBonus; 
     */
    public float CalculatePoints()
    {
        float basePoints = minPointsAt0Secs + (roundTime / baseRoundTime) * (baseRoundPoints - minPointsAt0Secs);
        float increasedPoints = basePoints * (1 + ((float)difficultyAdditivePercentageBonus / 100.0f) * cardsDifficulty + (Power((float)timeReductionMultiplicativePercentageBonus / 100.0f + 1f, reduceTimeAtThisStage - 1) - 1f));
        Debug.Log("kecske CalculatePoints: " + basePoints + " - " + ((float)difficultyAdditivePercentageBonus / 100.0f) * cardsDifficulty + " - " + (Power((float)timeReductionMultiplicativePercentageBonus / 100.0f + 1f, reduceTimeAtThisStage - 1) - 1f));
        return Mathf.Round(increasedPoints);
    }

    float Power(float value, int power)
    {
        float response = 1f;
        for(int i = 0; i < power; i++)
        {
            response *= value;
        }
        Debug.Log("kecske Power: " + value + " - " + power + " - " + response);
        return response;
    }

    public void EndLostRound()
    {
        roundTime = 0;
        roundEnded = true;
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().SetMusicSpeedToDefault();

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
