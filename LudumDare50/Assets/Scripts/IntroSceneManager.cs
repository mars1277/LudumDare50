using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSceneManager : MonoBehaviour
{
    public TMP_InputField playerName;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.GetString(LeaderBoardManager.PlayerName).Equals(""))
        {
            playerName.text = PlayerPrefs.GetString(LeaderBoardManager.PlayerName);
        }
        StartCoroutine(PlayIntroSound());
    }
    
    IEnumerator PlayIntroSound()
    {
        yield return new WaitForSeconds(0.5F);
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().PlayIntroMonsterSound();
    }
}
