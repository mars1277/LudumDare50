using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public string gameSceneName;

    public int roundCounter = 0;
    public float baseRoundTime = 10.0f;
    public float roundTime = 10.0f;
    public bool roundEnded = true;

    public TMP_Text roundCountText;
    public TMP_Text roundTimeText;
    public Image beastFaceImage;
    public GameObject cardsGroup;
    public GameObject cardGO;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Krisz")
        {
            roundEnded = true;
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
                roundTimeText.text = roundTime.ToString("F2");
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
        roundTime = baseRoundTime;
        roundTimeText.text = roundTime.ToString("F2");
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
        roundTime = baseRoundTime;
        roundTimeText.text = roundTime.ToString("F2");
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
        int cardsDifficulty = 1;
        //choose a card set from the card sets
        CardSets.CardSet cardSet = GameObject.Find("CardSets").GetComponent<CardSets>().GetRandomCardSetInRandomizedOrder(cardsDifficulty);
        //choose a card to be the face of the beast
        int beastFaceId = cardSet.GetBeastFaceId();
        //create and show cards in the game, one with win condition, one with lose one
        for (int i = 0; i < cardSet.cards.Count; i++)
        {
            GameObject card = Instantiate(cardGO, cardsGroup.transform);
            card.GetComponent<Image>().sprite = cardSet.cards[i];
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

    public float CalculateNewBaseRoundTime()
    {
        return baseRoundTime - 0.2f;
    }

    public void EndSuccessfulRound()
    {
        if (!roundEnded)
        {
            StartNewRound();
        }
    }

    public void EndLostRound()
    {
        roundTime = 0;
        roundEnded = true;
        roundTimeText.text = "You lost!";
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
