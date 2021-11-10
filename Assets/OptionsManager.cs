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
    [SerializeField]    private Slider              sliderAjustGeneralVolume;
    [SerializeField]    private GameObject          ButtonMuteMusic;
    [SerializeField]    private GameObject          ButtonMuteEffects;
    [SerializeField]    private Sprite              spriteMutated;
    [SerializeField]    private Sprite              spriteUnmuted;

    [Header("Atributtes Volumes")]
    [HideInInspector]   private bool                muteMusic;
    [HideInInspector]   private bool                muteEffects;
    


    void Start()
    {
        muteMusic = false;
        muteEffects = false;
        sliderAjustMusicGame.value = PlayerPrefs.GetFloat("VolumeMusicGame");
        sliderAjustEffectsGame.value = PlayerPrefs.GetFloat("VolumeEffectsGame");
        sliderAjustGeneralVolume.value = PlayerPrefs.GetFloat("VolumeGeneral");
    }

    void Update()
    {
        PlayerPrefs.SetFloat("VolumeGeneral", sliderAjustGeneralVolume.value);
        PlayerPrefs.SetFloat("VolumeMusicGame", sliderAjustMusicGame.value * sliderAjustGeneralVolume.value);
        PlayerPrefs.SetFloat("VolumeEffectsGame", sliderAjustEffectsGame.value * sliderAjustGeneralVolume.value);        
    }

    public void MuteMusic()
    {
        muteMusic ^= true;

        if(muteMusic)
        {
            PlayerPrefs.SetFloat("PastVolumeMusic", PlayerPrefs.GetFloat("VolumeMusicGame"));
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustMusicGame.value = 0;
            sliderAjustMusicGame.interactable = false;
        }else 
        {
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustMusicGame.interactable = true;
            sliderAjustMusicGame.value = PlayerPrefs.GetFloat("PastVolumeMusic");
        }
        
    }

    public void MuteEffects()
    {
        muteEffects ^= true;

        if(muteEffects)
        {
            PlayerPrefs.SetFloat("PastVolumeEffects", PlayerPrefs.GetFloat("VolumeEffectsGame"));
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustEffectsGame.value = 0;
            sliderAjustEffectsGame.interactable = false;
        }else 
        {
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustEffectsGame.interactable = true;
            sliderAjustEffectsGame.value = PlayerPrefs.GetFloat("PastVolumeEffects");

        }
    }
}

