using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public string gameSceneName;
    public float baseRoundTime = 10.0f;
    public int reduceRoundTimePerXRound;
    public float roundTimeReduction;
    public int increaseDifficultyPerXRound;

    public int roundCounter = 0;
    public float roundTime = 10.0f;
    public bool roundEnded = true;
    public int cardsDifficulty;
    float points;

    public TMP_Text roundCountText;
    public Slider roundTimerBar;
    public TMP_Text pointsText;
    public Image beastFaceImage;
    public GameObject cardsGroup;
    public GameObject cardGO;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Krisz")
        {
            roundEnded = true;
            points = 0;
            pointsText.text = "" + points;
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
        baseRoundTime = CalculateNewBaseRoundTime();
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
        cardsDifficulty = CalculateCardsDifficulty();
        //choose a card set from the card sets
        CardSets.CardSet cardSet = GameObject.Find("CardSets").GetComponent<CardSets>().GetRandomCardSetInRandomizedOrder(cardsDifficulty);
        //choose a card to be the face of the beast
        int beastFaceId = cardSet.GetBeastFaceId();
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
    }

    public int CalculateCardsDifficulty()
    {
        if (roundCounter > 1 && (roundCounter - 1) % increaseDifficultyPerXRound == 0)
        {
            cardsDifficulty++;
        }
        return cardsDifficulty;
    }

    public float CalculateNewBaseRoundTime()
    {
        if (roundCounter > 1 && (roundCounter - 1) % reduceRoundTimePerXRound == 0)
        {
            return baseRoundTime - roundTimeReduction;
        }
        return baseRoundTime;
    }

    public void EndSuccessfulRound()
    {
        if (!roundEnded)
        {
            float p = CalculatePoints();
            points += p;
            pointsText.text = "" + points;
            StartNewRound();
        }
    }

    public float CalculatePoints()
    {
        return Mathf.Round((roundTime / baseRoundTime) * (1 + 0.1f * (cardsDifficulty - 1)) * 100);
    }

    public void EndLostRound()
    {
        roundTime = 0;
        roundEnded = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
