using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text pointsText;
    public TMP_Text roundCountText;

    void Start()
    {
        pointsText.text = "" + EventManager.points;
        roundCountText.text = "" + EventManager.roundCounter;
    }
}
