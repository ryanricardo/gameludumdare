using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int LvlsWon;
    public GameObject[] levelsPanels;
    [SerializeField] GameObject panelMenu, panelLevls, panelLoading;
    [SerializeField] public TMP_Dropdown       selectLanguage;
    [SerializeField] GameObject[] buttonsLvls;
    [SerializeField] TextMeshProUGUI textDiamondsTotal,textPowerBonus1, textPowerBonus2;
    [SerializeField] TextMeshProUGUI[] textDiamondsLvls;
    [SerializeField] private Data data;
    int lvlsNivel = 16;

    private void Awake(){
        Screen.orientation = ScreenOrientation.Portrait;        
        panelLoading.SetActive(true);
        panelLevls.SetActive(true);
        selectLanguage.value = PlayerPrefs.GetInt("valeuLanguage");
        // Salva no vetor buttonLvls todos os prefabs dos botoes   
        buttonsLvls = GameObject.FindGameObjectsWithTag("buttonLvl");   
        // A partir da quantidade total de botoes no menu iguala o valor de lvl do botao de acordo com sua posição no menu
        for (int i = 1; i <= buttonsLvls.Length ; i++){
            buttonsLvls[i-1].GetComponent<ButtonLvl>().lvl = i; 
        }  
        StartCoroutine(StartMenu());         
    }

    IEnumerator StartMenu(){
        panelLevls.SetActive(false);        
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
        TextButtons();

        textPowerBonus1.text = "x" + PlayerPrefs.GetInt("Bonus1").ToString();
        textPowerBonus2.text = "x" + PlayerPrefs.GetInt("Bonus2").ToString();
        yield return new WaitForSecondsRealtime(3);
        panelLoading.SetActive(false);
        panelMenu.SetActive(true);
    }

    private void Update(){        
    }
    

    public void Play(){
        panelMenu.SetActive(false);
        panelLevls.SetActive(true);
    }

    public void ButtonExit(){
        Application.Quit();
    }
    
    public void ButtonBackMenu(){        
        panelLevls.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void ButtonReset(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MuteGeneral", -1);
        PlayerPrefs.SetInt("MuteMusic", -1);
        PlayerPrefs.SetInt("MuteEffects", -1);
        PlayerPrefs.SetInt("SkinRock1", 1);
        PlayerPrefs.SetInt("SkinRock2", 1);
        PlayerPrefs.SetInt("SkinRock3", 1);
        PlayerPrefs.SetInt("valueLanguage", 0);
        while(data.skinRock1.Count != 2)
        {
            data.skinRock1.RemoveAt(2);
            data.skinRock2.RemoveAt(2);
            data.skinRock3.RemoveAt(2);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void SelectLanguage()
    {
        switch(selectLanguage.value)
        {
            case 0:
                PlayerPrefs.SetString("Language","English");
                PlayerPrefs.SetInt("valeuLanguage", 0);
            break;

            case 1:
                PlayerPrefs.SetString("Language","Portuguese (Brazil)");
                PlayerPrefs.SetInt("valeuLanguage", 1);
            break;
        }
    }
    
    public void MusicOn(){
        PlayerPrefs.SetString("PPMusic", "ON");
    }
    public void MusicOff(){
        PlayerPrefs.SetString("PPMusic", "OFF");
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

    void TextButtons(){
        for (int i = 1; i <= buttonsLvls.Length ; i++){  
            if(i<=lvlsNivel){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = i.ToString(); 
            }
            if(i>lvlsNivel && i<=lvlsNivel*2){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel).ToString(); 
            }
            if(i>lvlsNivel*2 && i<=lvlsNivel*3){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*2).ToString(); 
            }
            if(i>lvlsNivel*3 && i<=lvlsNivel*4){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*3).ToString(); 
            }
            if(i>lvlsNivel*4 && i<=lvlsNivel*5){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*4).ToString(); 
            }
        }
    }
}
