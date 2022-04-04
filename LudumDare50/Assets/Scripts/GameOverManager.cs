using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text pointsText;
    public TMP_Text roundCountText;

    public Image beastFace;
    public List<Image> optionFrames;
    public List<Image> optionFaces;

    public Sprite wrongChoiceFrame;

    public int beastFaceMaxSize;
    public int cardFaceMaxSize;

    public string highScoreMessage;
    public string notHighScoreMessage;
    public TMP_Text resultMessage;

    void Start()
    {
        pointsText.text = "" + EventManager.points;
        roundCountText.text = "" + EventManager.roundCounter;

        beastFace.sprite = EventManager.actualCards[EventManager.actualBeastFaceId];
        SetImageSpriteSize(true, EventManager.actualCards[EventManager.actualBeastFaceId], beastFace);
        for (int i = 0; i < optionFrames.Count; i++)
        {
            if (EventManager.actualBeastFaceId != i)
            {
                optionFrames[i].sprite = wrongChoiceFrame;
            }
        }
        for (int i = 0; i < optionFaces.Count; i++)
        {
            optionFaces[i].sprite = EventManager.actualCards[i];
            SetImageSpriteSize(false, EventManager.actualCards[i], optionFaces[i]);
        }

        bool isNewHighScore = LeaderBoardManager.MadeItToTheLeaderBoardAndIfSoSaveIt(EventManager.points, EventManager.roundCounter);
        if (isNewHighScore)
        {
            resultMessage.text = highScoreMessage;
        }
        else
        {
            resultMessage.text = notHighScoreMessage;
        }
    }

    public void SetImageSpriteSize(bool isBeast, Sprite sprite, Image image)
    {
        int maxSize;
        if (isBeast)
        {
            maxSize = beastFaceMaxSize;
        }
        else
        {
            maxSize = cardFaceMaxSize;
        }
        float cardFaceSpriteWidth = sprite.rect.width;
        float cardFaceSpriteHeight = sprite.rect.height;
        if (cardFaceSpriteWidth > cardFaceSpriteHeight)
        {
            image.rectTransform.sizeDelta = new Vector2(maxSize, cardFaceSpriteHeight / cardFaceSpriteWidth * maxSize);
        }
        else
        {
            image.rectTransform.sizeDelta = new Vector2(cardFaceSpriteWidth / cardFaceSpriteHeight * maxSize, maxSize);
        }
    }
}
