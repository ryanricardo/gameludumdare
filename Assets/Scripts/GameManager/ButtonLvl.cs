using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int lvl, diamondsButton;
    public TextMeshProUGUI textButton;
    [SerializeField]GameObject[] diamodsSprites;
    LevelManager levelManager;
    Color colorButton;

    private void Awake(){
        levelManager = FindObjectOfType<LevelManager>();
        colorButton = GetComponent<Button>().colors.normalColor;
    }

    public void LoadLevel(){
        levelManager.PlayClipClickButton();
        Loading();
    }
    void Loading(){
        // levelManager.panelLoading.SetActive(true);
        // levelManager.panelLevls.SetActive(false);
        colorButton = Color.yellow;
        SceneManager.LoadScene(lvl);            
    }

    private void Start()
    {
        DiamondsCount();    
    }     


    void DiamondsCount(){
        string PPlvl = "DiamondsLvl" + lvl;        
        diamondsButton = PlayerPrefs.GetInt(PPlvl);
        
        switch (diamondsButton){
            case 0:
                diamodsSprites[0].SetActive(false);
                diamodsSprites[1].SetActive(false);
                diamodsSprites[2].SetActive(false);
                break;
            case 1:                
                diamodsSprites[0].SetActive(true);
                diamodsSprites[1].SetActive(false);
                diamodsSprites[2].SetActive(false);
                break;
            case 2:                
                diamodsSprites[0].SetActive(true);
                diamodsSprites[1].SetActive(true);
                diamodsSprites[2].SetActive(false);
                break;
            case 3:            
                diamodsSprites[0].SetActive(true);
                diamodsSprites[1].SetActive(true);
                diamodsSprites[2].SetActive(true);
                break;
        }
    }
}
