using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int lvl, diamondsButton;
    [SerializeField] GameObject[] diamodsSprites;

    
    public void LoadLevel(){
        SceneManager.LoadScene(lvl);            
    }

    private void Start()
    {
        string PPlvl = "PPDiamondsLvl-" + lvl;        
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
