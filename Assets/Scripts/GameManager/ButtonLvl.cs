using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int lvl, diamondsButton;
    public TextMeshProUGUI textButton;
    [SerializeField]GameObject[] diamodsSprites;

    LevelManager levelManager;
    GameManager gameManager;
    
    public void LoadLevel(){
        SceneManager.LoadScene(lvl);            
    }

    private void Awake()
    {        
        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
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
