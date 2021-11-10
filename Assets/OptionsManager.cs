using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private Slider              sliderAjustMusicGame;
    [SerializeField]    private Slider              sliderAjustEffectsGame;


    void Start()
    {
        sliderAjustMusicGame.value = PlayerPrefs.GetFloat("VolumeMusicGame");
        sliderAjustEffectsGame.value = PlayerPrefs.GetFloat("VolumeEffectsGame");

    }

    void Update()
    {
        PlayerPrefs.SetFloat("VolumeMusicGame", sliderAjustMusicGame.value);
        PlayerPrefs.SetFloat("VolumeEffectsGame", sliderAjustEffectsGame.value);        
    }
}
