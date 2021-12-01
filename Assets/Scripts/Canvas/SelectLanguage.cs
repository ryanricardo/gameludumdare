using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectLanguage : MonoBehaviour
{

    [HideInInspector]   private TextMeshProUGUI     textObject;
    [SerializeField]    private string[]            insertText;
    

    void Start()
    {

        textObject = GetComponent<TextMeshProUGUI>();


        switch(PlayerPrefs.GetString("Language"))
        {
            case "English":
            textObject.text = insertText[0];
            break;

            case "Portuguese (Brazil)":
            textObject.text = insertText[1];
            break;
        }
    }

    void Update()
    {
        
    }
}
