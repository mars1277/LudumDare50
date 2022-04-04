using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
