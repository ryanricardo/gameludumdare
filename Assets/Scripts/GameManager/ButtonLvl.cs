using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int nivel, lvl, diamondsButton;
    public TextMeshProUGUI textButton;
    [SerializeField] GameObject[] diamodsSprites;
    LevelManager levelManager;
    Color colorButton;
    PanelLevel panelLevel;

    private void Awake(){
        levelManager = FindObjectOfType<LevelManager>();
        colorButton = GetComponent<Button>().colors.normalColor;
        panelLevel = GetComponentInParent<PanelLevel>();
    }
    private void Start()
    {
        DiamondsCount();    
    }     

    public void LoadLevel(){
        levelManager.PlayClipClickButton();        
        colorButton = Color.yellow;
        panelLevel.textPanelLoading.SetActive(true);
        SceneManager.LoadScene(lvl);          
        // Loading();
    }
    void Loading(){
        // levelManager.panelLoading.SetActive(true);
        // levelManager.panelLevls.SetActive(false);  
    }

    void DiamondsCount(){
        string PPlvl = "DiamondsLvl" + lvl;        
        diamondsButton = PlayerPrefs.GetInt("DiamondsLvl" + lvl);
        
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
