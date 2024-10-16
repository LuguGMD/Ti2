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

    public void PlayEnemySounds(float fadeOutStart)
    {
        StopAllCoroutines();
        audioMixer.SetFloat("EnemySoundsVolume", 0);  // Unmutes EnemySounds audio mixer group

        StartCoroutine(FadeOut(fadeOutStart));  // The EnemySounds audio mixer group stays unmuted for the duration of the hit enemy's note 
    }

    public IEnumerator FadeOut(float fadeOutStart)
    {
        yield return new WaitForSeconds(fadeOutStart);
        float time = 0;
        float fadeOutDuration = 0.5f;
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