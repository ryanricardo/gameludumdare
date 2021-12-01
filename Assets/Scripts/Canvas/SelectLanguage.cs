using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLanguage : MonoBehaviour
{
    [HideInInspector]   private LevelManager        levelManager;
    [HideInInspector]   private TextMeshProUGUI     textObject;
    [SerializeField]    private string              insertTextEnglish;
    [SerializeField]    private string              insertTextPortugueseBrazil;

    

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        textObject = GetComponent<TextMeshProUGUI>();
        ChangeLanguage();


    }
    
    public void ChangeLanguage()
    {
         
       
            PlayerPrefs.SetInt("valueLanguage", PlayerPrefs.GetInt("valeuLanguage"));
        

        switch(PlayerPrefs.GetInt("valueLanguage"))
        {
            case 0:
            textObject.text = insertTextEnglish;
            break;

            case 1:
            textObject.text = insertTextPortugueseBrazil;
            break;
        }
    }
    void Update()
    {
        
    }
}
