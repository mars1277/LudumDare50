using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayIntroSound());
    }
    
    IEnumerator PlayIntroSound()
    {
        yield return new WaitForSeconds(0.5F);
        GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>().PlayIntroMonsterSound();
    }
}
