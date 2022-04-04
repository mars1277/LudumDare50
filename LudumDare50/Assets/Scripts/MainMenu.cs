using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    void Start()
    {
        if (!PlayerPrefs.HasKey(LeaderBoardManager.PlayerName))
        {
            PlayerPrefs.SetString(LeaderBoardManager.PlayerName, "");
        }
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
        PlayerPrefs.SetString(LeaderBoardManager.PlayerName, playerName.text);
        LeaderBoardManager.NewestHighScoreIndex = -1;
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().StopIntroMonsterSound();
        SceneManager.LoadScene("Game");
    }

    public void LeaderBoard()
    {
      SceneManager.LoadScene("LeaderBoard");
    }

}
