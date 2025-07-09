using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource bgAudioSoruce;
    [SerializeField] private AudioSource enemiesAudioSoruce;
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip[] bgMusicClips;
    [SerializeField] private AudioClip[] sfxClips;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

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
        // Sets audio groups volume and set volume sliders to saved values
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (musicSlider != null) musicSlider.value = volume;
        audioMixer.SetFloat("MusicVolume", ConvertToDB(volume));

        volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        if (sfxSlider != null) sfxSlider.value = volume;
        audioMixer.SetFloat("SFXVolume", ConvertToDB(volume));
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
        audioMixer.SetFloat("EnemySoundsVolume", 0);  // Unmutes EnemySounds audio mixer group

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
            audioMixer.SetFloat("EnemySoundsVolume", Mathf.Lerp(startVolume, -5, time / fadeOutDuration));
            time += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void MuteEnemySounds()
    {
        enemiesAudioSoruce.mute = true;
    }

    public void PlaySFX(string fileName)
    {
        int index = FindSFXIndex(fileName);
        if (index != -1)
        {
            sfxAudioSource.pitch = Random.Range(0.9f, 1.2f);
            sfxAudioSource.PlayOneShot(sfxClips[index]);
        }
    }

    private int FindSFXIndex(string fileName)
    {
        int index = -1;

        for (int i = 0; i < sfxClips.Length; i++)
        {
            if (sfxClips[i].name == fileName)
            {
                index = i;
            }
        }

        return index;
    }

    public void UpdateMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        audioMixer.SetFloat("MusicVolume", ConvertToDB(value));
    }

    public void UpdateSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        audioMixer.SetFloat("SFXVolume", ConvertToDB(value));
    }
}
