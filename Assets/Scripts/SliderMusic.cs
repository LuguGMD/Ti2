using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        slider.value = volume;
    }

    public void UpdateVolumeMusic(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}
