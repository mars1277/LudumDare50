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

    void Start()
    {
        pointsText.text = "" + EventManager.points;
        roundCountText.text = "" + EventManager.roundCounter;

        beastFace.sprite = EventManager.actualCards[EventManager.actualBeastFaceId];
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
        }
    }
}
