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
    [SerializeField]    GameObject                  ButtonMuteMusic;
    [SerializeField]    GameObject                  ButtonMuteEffects;
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

    }

    void Update()
    {
        PlayerPrefs.SetFloat("VolumeMusicGame", sliderAjustMusicGame.value);
        PlayerPrefs.SetFloat("VolumeEffectsGame", sliderAjustEffectsGame.value);        
    }

    public void MuteMusic()
    {
        muteMusic ^= true;

        if(muteMusic)
        {
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustMusicGame.value = 0;
            sliderAjustMusicGame.interactable = false;
        }else 
        {
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustMusicGame.interactable = true;
            sliderAjustMusicGame.value = PlayerPrefs.GetFloat("VolumeMusicGame");
        }
        
    }

    public void MuteEffects()
    {
        muteEffects ^= true;

        if(muteEffects)
        {
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustEffectsGame.value = 0;
            sliderAjustEffectsGame.interactable = false;
        }else 
        {
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustEffectsGame.interactable = true;
            sliderAjustEffectsGame.value = PlayerPrefs.GetFloat("VolumeEffectsGame");

        }
    }
}

