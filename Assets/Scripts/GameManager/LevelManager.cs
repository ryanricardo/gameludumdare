using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int levelPanelNumber = 0, LvlsWon;
    [SerializeField] GameObject panelMenu, panelOptions, panelMarket, panelInventory;
    [SerializeField] GameObject[] levelsPanels, buttonsLvls;
    [SerializeField] TextMeshProUGUI textDiamondsTotal;
    [SerializeField] TextMeshProUGUI[] textDiamondsLvls;
    [HideInInspector] private bool openInventory, openOptions;
    GameManager gm; Data data;

    private void Awake(){
        // PlayerPrefs.DeleteAll();
        

        gm = FindObjectOfType<GameManager>();
        data = FindObjectOfType<Data>();

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

        openInventory = false;
        openOptions = false;

        // Desativa os gameObjects dos paineis 
        foreach (var item in levelsPanels){
            item.SetActive(false);
        }

        // Verifica se existe algum valor para PPLvlsWon e salva em LvlsWon se não LvlsWon deve ser igual a 1
        if(!PlayerPrefs.HasKey("LvlsWon")){
            LvlsWon = 1;
        }else{
            LvlsWon = PlayerPrefs.GetInt("LvlsWon");      // Pega o valor de PPLvlsWon para saber a fase em que o jogador chegou 
        }

        // Sabendo o valor de PPLvlsWon libera o click nos botoes das fases abaixo e igual ao seu valor
        for (int i = 0; i < buttonsLvls.Length; i++) {
            if(PlayerPrefs.GetInt("LvlsWon") <= i){
                buttonsLvls[i].GetComponent<Button>().interactable = false;
            }
        }
        buttonsLvls[0].GetComponent<Button>().interactable = true;
        Diamonds();

        levelPanelNumber = 0;
    }

    public void Play(){
        panelMenu.SetActive(false);
        levelsPanels[0].SetActive(true);
    }

    public void ButtonExit(){
        Application.Quit();
    }

    public void ButtonInventory()
    {
        openInventory ^= true;
        panelInventory.gameObject.SetActive(openInventory);
    }

    public void ButtonOptions()
    {
        openOptions ^= true;
        panelOptions.gameObject.SetActive(openOptions);
    }
    
    public void ButtonBackMenu(){        
        // panelMarket.SetActive(false);
        // panelOptions.SetActive(false);
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

    void Diamonds(){
        // Repassa os valores dos diamantes por nivel para seus devidos textos
        for (int i = 0; i < 6; i++){
            int f = i + 1;
            textDiamondsLvls[i].text = "x" + PlayerPrefs.GetInt("Diamonds" + f);            
        }

        // Somatorio dos valores de diamantes por nivel
        int n3 = PlayerPrefs.GetInt("Diamonds1") + PlayerPrefs.GetInt("Diamonds2") + PlayerPrefs.GetInt("Diamonds3") + PlayerPrefs.GetInt("Diamonds4") + PlayerPrefs.GetInt("Diamonds5") + PlayerPrefs.GetInt("Diamonds6");
        textDiamondsTotal.text = "x" + n3;
    }
}
