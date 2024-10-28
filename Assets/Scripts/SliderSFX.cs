using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSFX : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        slider.value = volume;
    }

    public void UpdateVolumeSFX(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
