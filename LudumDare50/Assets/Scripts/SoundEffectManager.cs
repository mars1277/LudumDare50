using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource cardPulling;

    public List<AudioSource> monsterSounds;
    int previousMonsterSoundId;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayCardPullingSound()
    {
        cardPulling.Play();
    }

    public void PlayRandomMonsterSound()
    {
        int randomMosterSoundId = Random.Range(0, monsterSounds.Count);
        while (previousMonsterSoundId == randomMosterSoundId)
        {
            randomMosterSoundId = Random.Range(0, monsterSounds.Count);
        }
        previousMonsterSoundId = randomMosterSoundId;
        Debug.Log("monster sound id: " + randomMosterSoundId);
        monsterSounds[randomMosterSoundId].Play();
    }
}
