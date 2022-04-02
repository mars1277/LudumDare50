using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    public int roundCounter = 0;
    public float roundTime = 10.0f;

    public TMP_Text roundCountText;
    public Image beastFaceImage;
    public Image card1Image;
    public Image card2Image;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartNewRound()
    {
        //set new round count
        roundCounter++;
        roundCountText.text = "" + roundCounter;
        //get card sets based on round
        int cardsDifficulty = 1;
        //choose a card set from the card sets
        CardSets.CardSet cardSet = GameObject.Find("CardSets").GetComponent<CardSets>().GetRandomCardSetInRandomizedOrder(cardsDifficulty);
        //choose a card to be the face of the beast
        Sprite beastFace = cardSet.GetBeastFaceCard();
        beastFaceImage.sprite = beastFace;
        //create and show cards in the game, one with win condition, one with lose one
        card1Image.sprite = cardSet.cards[0];
        card2Image.sprite = cardSet.cards[1];

        //start round
    }

    public void EndSuccessfulRound()
    {

    }

    public void EndLostRound()
    {

    }
}
