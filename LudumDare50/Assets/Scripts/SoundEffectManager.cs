using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource introMonsterSound;

    public AudioSource cardPulling;

    public List<AudioSource> monsterSounds;
    int previousMonsterSoundId;

    public AudioSource click;

    public AudioSource music;
    public float musicSpeed = 1f;
    public float maxMusicSpeed = 2f;
    public float increaseMusicSpeedValue;
    public float settingMusicSpeedToDefaultInSecs = 1f;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        music.pitch = musicSpeed;
        music.outputAudioMixerGroup.audioMixer.SetFloat("PitchShifter", 1f / musicSpeed);
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void IncreaseMusicSpeed()
    {
        if (musicSpeed < maxMusicSpeed)
        {
            musicSpeed += increaseMusicSpeedValue;
        }
    }

    public void SetMusicSpeedToDefault()
    {
        StartCoroutine(SetSpeedToDefault());
    }

    IEnumerator SetSpeedToDefault()
    {
        int ticks = 100;
        float musicSpeedReduction = musicSpeed - 1f;
        for (int i = 0; i < ticks; i++)
        {
            yield return new WaitForSeconds(settingMusicSpeedToDefaultInSecs / (float)ticks);
            musicSpeed -= musicSpeedReduction / (float)ticks;
        }
    }

    public void PlayIntroMonsterSound()
    {
        introMonsterSound.Play();
    }

    public void StopIntroMonsterSound()
    {
        introMonsterSound.Stop();
    }

    public void PlayCardPullingSound()
    {
        cardPulling.Play();
    }

    public void Click()
    {
      click.Play();
    }

    public void PlayRandomMonsterSound()
    {
        int randomMosterSoundId = Random.Range(0, monsterSounds.Count);
        while (previousMonsterSoundId == randomMosterSoundId)
        {
            randomMosterSoundId = Random.Range(0, monsterSounds.Count);
        }
        previousMonsterSoundId = randomMosterSoundId;
        monsterSounds[randomMosterSoundId].Play();
    }
}
