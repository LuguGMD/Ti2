using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource bgAudioSoruce;
    [SerializeField] private AudioSource enemiesAudioSoruce;
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip[] bgMusicClips;
    [SerializeField] private AudioClip[] sfxClips;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        // Sets audio groups volume
        float volume = ConvertToDB(PlayerPrefs.GetFloat("MusicVolume", 1));
        audioMixer.SetFloat("MusicVolume", volume);
        volume = ConvertToDB(PlayerPrefs.GetFloat("SFXVolume", 1));
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public float ConvertToDB(float value)
    {
        return Mathf.Log10(value) * 20;
    }

    public void PlayMusic()
    {
        bgAudioSoruce.Play();
        enemiesAudioSoruce.Play();
    }

    public void PauseMusic()
    {
        bgAudioSoruce.Pause();
        enemiesAudioSoruce.Pause();
    }

    public void ChangeBGMusic(int id)
    {
        bgAudioSoruce.Stop();
        bgAudioSoruce.clip = bgMusicClips[id];
        bgAudioSoruce.Play();
    }

    public void PlayEnemySounds(float fadeOutStart)
    {
        // Unmutes the part of the soundtrack composed by the enemies

        StopAllCoroutines();
        audioMixer.SetFloat("EnemySoundsVolume", 5);  // Unmutes EnemySounds audio mixer group

        StartCoroutine(FadeOut(fadeOutStart));  // The EnemySounds audio mixer group stays unmuted for the duration of the hit enemy's note 
        //StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake()); // Activates Camera Shake
    }

    public void StopEnemySounds()
    {
        StartCoroutine(FadeOut(0));
    }

    public IEnumerator FadeOut(float fadeOutStart)
    {
        float time = 0;
        float fadeOutDuration = 0.5f;

        yield return new WaitForSeconds(fadeOutStart);
        audioMixer.GetFloat("EnemySoundsVolume", out float startVolume);
        while (time < fadeOutDuration)
        {
            audioMixer.SetFloat("EnemySoundsVolume", Mathf.Lerp(startVolume, -80, time / fadeOutDuration));
            time += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
