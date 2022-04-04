using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        for (int i = 1; i <= LeaderBoardManager.MaxLeaderBoardSlotSaved; i++)
        {
            if (!PlayerPrefs.HasKey(LeaderBoardManager.SlotNamePrefix + i))
            {
                PlayerPrefs.SetString(LeaderBoardManager.SlotNamePrefix + i, "");
            }
            if (!PlayerPrefs.HasKey(LeaderBoardManager.SlotScorePrefix + i))
            {
                PlayerPrefs.SetInt(LeaderBoardManager.SlotScorePrefix + i, 0);
            }
            if (!PlayerPrefs.HasKey(LeaderBoardManager.SlotRoundPrefix + i))
            {
                PlayerPrefs.SetInt(LeaderBoardManager.SlotRoundPrefix + i, 0);
            }
        }
    }

    public void PlayGame()
    {
      SceneManager.LoadScene("Intro");
    }

    public void QuitGame ()
    {
      Debug.Log("Quit");
      Application.Quit();
    }

    public void NextScene()
    {
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().StopIntroMonsterSound();
        SceneManager.LoadScene("Game");
    }

    public void LeaderBoard()
    {
      SceneManager.LoadScene("LeaderBoard");
    }

}
