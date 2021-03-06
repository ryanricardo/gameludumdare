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
    public GameObject panelMenu, panelLevls, panelLoading, toggleTutorial, scrollbarMenu, toggleLockLvls;
    [SerializeField] private float scrolValue = .667f;
    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioClip[] musicsLofi;         
    [SerializeField] private GameObject[] buttonsLvls;
    [SerializeField] private TextMeshProUGUI textDiamondsTotal, textPowerBonus1, textPowerBonus2;
    [SerializeField] private TextMeshProUGUI[] textDiamondsLvls;
    [SerializeField] private AudioSource sourceEffects;
    [SerializeField] private AudioClip clipClickButton;
    [SerializeField] private  Data data;
    int lvlsNivel = 20;

    private void Awake(){
        Screen.orientation = ScreenOrientation.Portrait;   
        scrollbarMenu.GetComponent<Scrollbar>().value = scrolValue;
        if(PlayerPrefs.GetString("Tutorial") == "tutorialOFF"){
            toggleTutorial.GetComponent<Toggle>().isOn = false;
        }else{
            toggleTutorial.GetComponent<Toggle>().isOn = true;
        } 
        panelLoading.SetActive(true);
        panelLevls.SetActive(true);
        // Salva no vetor buttonLvls todos os prefabs dos botoes   
        buttonsLvls = GameObject.FindGameObjectsWithTag("buttonLvl");   
        // A partir da quantidade total de botoes no menu iguala o valor de lvl do botao de acordo com sua posição no menu
        for (int i = 1; i <= buttonsLvls.Length ; i++){
            buttonsLvls[i-1].GetComponent<ButtonLvl>().lvl = i; 
        }  
        StartCoroutine(StartMenu());     
        StartCoroutine(FinishLoading());    
    }

    void Start()
    {        
        if(Time.realtimeSinceStartup==1){
            PlayerPrefs.SetInt("mortes",0);
            // PlayerPrefs.SetInt("mortesParaAnuncio",3);
            PlayerPrefs.SetFloat("vitorias",0);
            // PlayerPrefs.SetInt("vitoriasParaAnuncio",2);
        }
        sourceMusic.clip = musicsLofi[Random.Range(1, musicsLofi.Length)];
        sourceMusic.Play();
        Diamonds();
    }

    IEnumerator StartMenu(){
        // GameObject[] textsLoading = GameObject.FindGameObjectsWithTag("textPanelLevel");
        // foreach (var item in textsLoading){
        //     item.SetActive(false);
        // }
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
        
        TextButtons();
        textPowerBonus1.text = "x" + PlayerPrefs.GetInt("Bonus1").ToString();
        textPowerBonus2.text = "x" + PlayerPrefs.GetInt("Bonus2").ToString();
        yield return new WaitForSecondsRealtime(1);
    }

    IEnumerator FinishLoading()
    {
        yield return new WaitForSecondsRealtime(3);
        panelLoading.SetActive(false);
        panelMenu.SetActive(true);

    }

    private void Update()
    {        
        sourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
    }
    

    public void PlayClipClickButton()
    {
        sourceEffects.PlayOneShot(clipClickButton);
    }

    public void Play(){
        panelMenu.SetActive(false);
        panelLevls.SetActive(true);
        PlayClipClickButton();
        
    }

    public void ButtonExit(){
        Application.Quit();
        PlayClipClickButton();
    }
    
    public void ButtonBackMenu(){        
        panelLevls.SetActive(false);
        panelMenu.SetActive(true);
        PlayClipClickButton();
    }

    public void ButtonReset(){
        PlayClipClickButton();
        PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("MuteGeneral", -1);
        PlayerPrefs.SetInt("MuteMusic", -1);
        PlayerPrefs.SetInt("MuteEffects", -1);
        // PlayerPrefs.SetFloat("VolumeGeneral", 0.5f);
        PlayerPrefs.SetFloat("VolumeMusicGame", 0.5f);
        PlayerPrefs.SetFloat("VolumeEffectsGame", 0.5f);
        PlayerPrefs.SetInt("SkinRock1", 1);
        PlayerPrefs.SetInt("SkinRock2", 1);
        PlayerPrefs.SetInt("SkinRock3", 1);
        PlayerPrefs.SetInt("valueLanguage", 0);
        PlayerPrefs.SetString("Tutorial", "tutorialON");
        for(int i = 0; i < 100; i++)
        {
            PlayerPrefs.GetInt("PlayAgain" + i, 0);
        }
        ;while(data.skinRock1.Count != 2)
        {
            data.skinRock1.RemoveAt(2);
            data.skinRock2.RemoveAt(2);
            data.skinRock3.RemoveAt(2);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    
    public void MusicOn(){
        PlayerPrefs.SetString("PPMusic", "ON");
    }
    public void MusicOff(){
        PlayerPrefs.SetString("PPMusic", "OFF");
    }

    public void ToggleTutorial(){ 
        PlayClipClickButton();       
        if(toggleTutorial.GetComponent<Toggle>().isOn){
            PlayerPrefs.SetString("Tutorial", "tutorialON");
        }else{
            PlayerPrefs.SetString("Tutorial", "tutorialOFF");
        }
    }
    
    public void ToggleLockLevels(){        
        PlayClipClickButton();       
        // Se o toggle estiver desativado desbloqueia todas as fases se não bloqueia de acordo com a variavel LvlsWon
        if(toggleLockLvls.GetComponent<Toggle>().isOn==false){
            for (int i = 0; i < buttonsLvls.Length; i++){
                buttonsLvls[i].GetComponent<Button>().interactable = true;
            }
        }else{
            for (int i = 0; i < buttonsLvls.Length; i++){
                if(PlayerPrefs.GetInt("LvlsWon") <= i){
                    buttonsLvls[i].GetComponent<Button>().interactable = false;
                }
                buttonsLvls[0].GetComponent<Button>().interactable = true;
            }
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
        // textDiamondsTotal.text = "x" + n3;
        PlayerPrefs.SetInt("DiamondsTotal", n3);
        textDiamondsTotal.text = "x" + PlayerPrefs.GetInt("DiamondsTotal");
    }

    void TextButtons(){
        for (int i = 1; i <= buttonsLvls.Length ; i++){  
            if(i<=lvlsNivel){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = i.ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 1;
            }
            if(i>lvlsNivel && i<=lvlsNivel*2){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel).ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 2;
            }
            if(i>lvlsNivel*2 && i<=lvlsNivel*3){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*2).ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 3;
            }
            if(i>lvlsNivel*3 && i<=lvlsNivel*4){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*3).ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 4;
            }
            if(i>lvlsNivel*4 && i<=lvlsNivel*5){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*4).ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 5;
            }
            if(i>lvlsNivel*4 && i<=lvlsNivel*6){
                buttonsLvls[i-1].GetComponent<ButtonLvl>().textButton.text = (i-lvlsNivel*5).ToString(); 
                buttonsLvls[i-1].GetComponent<ButtonLvl>().nivel = 6;
            }
        }
    }
}
