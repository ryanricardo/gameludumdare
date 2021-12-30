using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private Slider              sliderAjustMusicGame;
    [SerializeField]    private Slider              sliderAjustEffectsGame;
    // [SerializeField]    private Slider              sliderAjustGeneralVolume;
    [SerializeField]    private GameObject          ButtonMuteMusic;
    [SerializeField]    private GameObject          ButtonMuteEffects;
    // [SerializeField]    private GameObject          ButtonMuteGeneral;
    [SerializeField]    private Sprite              spriteMutated;
    [SerializeField]    private Sprite              spriteUnmuted;
    [HideInInspector]   private LevelManager        levelManager;
    public TMP_Dropdown selectLanguage;


    void Awake()
    {

    }
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        selectLanguage.value = PlayerPrefs.GetInt("valeuLanguage");


        if(!PlayerPrefs.HasKey("MuteEffects") && !PlayerPrefs.HasKey("MuteMusic")){            
            PlayerPrefs.SetInt("MuteMusic", 1);
            PlayerPrefs.SetInt("MuteEffects", 1);
        }

        if(PlayerPrefs.GetInt("MuteMusic") == 1)
        {
            PlayerPrefs.SetFloat("PastVolumeMusic", PlayerPrefs.GetFloat("VolumeMusicGame"));
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustMusicGame.value = 0;
            sliderAjustMusicGame.interactable = false;
        }else 
        {
            ButtonMuteMusic.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustMusicGame.interactable = true;
        }

        if(PlayerPrefs.GetInt("MuteEffects") == 1)
        {
            PlayerPrefs.SetFloat("PastVolumeEffects", PlayerPrefs.GetFloat("VolumeEffectsGame"));
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteMutated;
            sliderAjustEffectsGame.value = 0;
            sliderAjustEffectsGame.interactable = false;
        }else 
        {
            ButtonMuteEffects.gameObject.GetComponent<Image>().sprite = spriteUnmuted;
            sliderAjustEffectsGame.interactable = true;

        }

        // if(PlayerPrefs.GetInt("MuteGeneral") == 1)
        // {
        //     PlayerPrefs.SetFloat("PastVolumeGeneral", PlayerPrefs.GetFloat("Volume"));
        //     ButtonMuteGeneral.GetComponent<Image>().sprite = spriteMutated;
        //     sliderAjustGeneralVolume.value = 0;
        //     sliderAjustGeneralVolume.interactable = false;
        // }else 
        // {
        //     ButtonMuteGeneral.GetComponent<Image>().sprite = spriteUnmuted;
        //     sliderAjustGeneralVolume.interactable = true;

        // }

        sliderAjustMusicGame.value = PlayerPrefs.GetFloat("VolumeMusicGame");
        sliderAjustEffectsGame.value = PlayerPrefs.GetFloat("VolumeEffectsGame");
        // sliderAjustGeneralVolume.value = PlayerPrefs.GetFloat("VolumeGeneral");

    }


    void FixedUpdate()
    {
        PlayerPrefs.SetFloat("VolumeMusicGame", sliderAjustMusicGame.value);
        PlayerPrefs.SetFloat("VolumeEffectsGame", sliderAjustEffectsGame.value);   
    }

    // public void MuteGeneral()
    // {
    //     levelManager.PlayClipClickButton();
    //     PlayerPrefs.SetInt("MuteGeneral", PlayerPrefs.GetInt("MuteGeneral") * -1);

    //     if(PlayerPrefs.GetInt("MuteGeneral") == 1)
    //     {
    //         PlayerPrefs.SetFloat("PastVolumeGeneral", PlayerPrefs.GetFloat("Volume"));
    //         ButtonMuteGeneral.GetComponent<Image>().sprite = spriteMutated;
    //         sliderAjustGeneralVolume.value = 0;
    //         sliderAjustGeneralVolume.interactable = false;
    //     }else 
    //     {
    //         ButtonMuteGeneral.GetComponent<Image>().sprite = spriteUnmuted;
    //         sliderAjustGeneralVolume.interactable = true;
    //         sliderAjustGeneralVolume.value = PlayerPrefs.GetFloat("PastVolumeGeneral");

    //     }
    // }

    public void SelectLanguage()
    {
        switch(selectLanguage.value)
        {
            case 0:
                levelManager.PlayClipClickButton();
                PlayerPrefs.SetString("Language","English");
                PlayerPrefs.SetInt("valeuLanguage", 0);
                break;
            case 1:                    
                levelManager.PlayClipClickButton();
                PlayerPrefs.SetString("Language","Portuguese (Brazil)");
                PlayerPrefs.SetInt("valeuLanguage", 1);
                break;            
        }
        // SceneManager.LoadScene(0);        
    }

    public void MuteMusic()
    {
        levelManager.PlayClipClickButton();
        PlayerPrefs.SetInt("MuteMusic", PlayerPrefs.GetInt("MuteMusic") * -1);

        if(PlayerPrefs.GetInt("MuteMusic") == 1)
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
        levelManager.PlayClipClickButton();
        PlayerPrefs.SetInt("MuteEffects", PlayerPrefs.GetInt("MuteEffects") * -1);

        if(PlayerPrefs.GetInt("MuteEffects") == 1)
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

