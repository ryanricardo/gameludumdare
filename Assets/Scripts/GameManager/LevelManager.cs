using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelPanelNumber = 0, LvlsWon;
    [SerializeField] GameObject panelMenu, panelOptions, panelMarket;
    [SerializeField] GameObject[] levelsPanels, buttonsLvls;

    private void Awake(){
        // Ativa os gameObjects dos paineis 
        foreach (var item in levelsPanels){
            item.SetActive(true);
        }
        // Salva no vetor buttonLvls todos os prefabs dos botoes   
        buttonsLvls = GameObject.FindGameObjectsWithTag("buttonLvl");   
        // A partir da quantidade total de botoes no menu iguala o valor de lvl do botao de acordo com sua posição no menu
        for (int i = 1; i <= buttonsLvls.Length ; i++){
            buttonsLvls[i-1].GetComponent<ButtonLvl>().lvl = i;
        }
    }

    private void Start(){
        // Desativa os gameObjects dos paineis 
        foreach (var item in levelsPanels){
            item.SetActive(false);
        }

        // Verifica se existe algum valor para PPLvlsWon e salva em LvlsWon se não LvlsWon deve ser igual a 1
        if(!PlayerPrefs.HasKey("PPLvlsWon")){
            LvlsWon = 1;
        }else{
            LvlsWon = PlayerPrefs.GetInt("PPLvlsWon");      // Pega o valor de PPLvlsWon para saber a fase em que o jogador chegou 
        }

        // Sabendo o valor de PPLvlsWon libera o click nos botoes das fases abaixo e igual ao seu valor
        for (int i = 0; i < buttonsLvls.Length; i++) {
            if(PlayerPrefs.GetInt("PPLvlsWon") <= i){
                buttonsLvls[i].GetComponent<Button>().interactable = false;
            }
        }
        buttonsLvls[0].GetComponent<Button>().interactable = true;
        
        levelPanelNumber = 0;
    }

    public void Play(){
        panelMenu.SetActive(false);
        levelsPanels[0].SetActive(true);
    }

    public void ButtonExit(){
        Application.Quit();
    }
    
    public void ButtonBackMenu(){        
        panelMarket.SetActive(false);
        panelOptions.SetActive(false);
        levelsPanels[levelPanelNumber].SetActive(false);
        panelMenu.SetActive(true);
    }
    
    public void MusicOn(){
        PlayerPrefs.SetString("PPMusic", "ON");
    }
    public void MusicOff(){
        PlayerPrefs.SetString("PPMusic", "OFF");
    }

    public void ArrowForward(){
        if(levelPanelNumber == levelsPanels.Length-1){            
            levelsPanels[levelPanelNumber].SetActive(false);
            levelsPanels[0].SetActive(true);
            levelPanelNumber = 0;
        }else{
            levelsPanels[levelPanelNumber].SetActive(false);
            levelsPanels[levelPanelNumber+1].SetActive(true);
            levelPanelNumber += 1;
        }
    }
    public void ArrowBackward(){    
        if(levelPanelNumber == 0){              
            levelsPanels[0].SetActive(false);
            levelsPanels[levelsPanels.Length-1].SetActive(true);
            levelPanelNumber = levelsPanels.Length-1;
        }else{   
            levelsPanels[levelPanelNumber].SetActive(false);
            levelsPanels[levelPanelNumber-1].SetActive(true);
            levelPanelNumber -= 1;
        }
    }

}