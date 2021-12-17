using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasPlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private Slider              sliderBalance;
    [SerializeField]    private TextMeshProUGUI     textLevel, textLevelState, textTimer, textTimer0, textTimer1, textTimerFinish, textSkin, textBonusAmount;
    [SerializeField]    private GameObject          panelLoading, panelPlay, panelPauseFinish, buttonNext, buttonBack, buttonPause, touchControllers;
    [SerializeField]    private GameObject[]        diamondsSprites, bonus, buttonsBonus;  
    [SerializeField]    private TextMeshProUGUI[]   textBonusPause, textBonusPlay;
    [SerializeField]    private Tutorial            tutorial;
    [SerializeField]    public  AudioClip           clipClickButtonMenu, clipTakeSkin, clipFinishLvl, clipGameOverLvl;
    [SerializeField]    public  AudioSource         sourceEffectsMenu;
    [HideInInspector]   private GameManager         gm;
    [HideInInspector]   private NewPlayerController playerController;
    [HideInInspector]   private Data data;

    
    [Header("Atributtes Timer")]
    [SerializeField] private float timer;
    private float timeDiamond;

    [Header("Atributtes Rewards")]
    [SerializeField]    private int bonusNumber;
    [SerializeField]    private int bonusAmount;

    [Header("Atributtes Controllers")]
    [HideInInspector]   private bool                jump;

    [HideInInspector] public bool bonusReceived;
    private bool useBonus;
    private GameObject panelBonus, panelSkin;

    private void Awake(){
        LevelState(GameManager.State.LOADING);
        playerController = FindObjectOfType<NewPlayerController>();
        gm = FindObjectOfType<GameManager>();
        data = FindObjectOfType<Data>();
        buttonNext.GetComponent<Button>().interactable = true;
        StartCoroutine(StartLevel());
        sourceEffectsMenu.volume = PlayerPrefs.GetFloat("VolumeEffectsGame");
    }

    IEnumerator StartLevel(){
        TextLevel();
        useBonus = true;
        string PPlvl = "DiamondsLvl" + gm.currentScene;
        timeDiamond = SceneManager.GetActiveScene().buildIndex * 5 + 25;
        for (int i = 0; i < diamondsSprites.Length; i++){
            diamondsSprites[i].SetActive(false);
        }
        if(PlayerPrefs.GetInt(PPlvl)==3){
            bonusReceived = true;
        }else{
            bonusReceived = false;
        }
        if(GameObject.Find("PanelBonus")!=null){
            panelBonus = GameObject.Find("PanelBonus");
            panelBonus.SetActive(false);
        }else if(GameObject.Find("PanelSkin")!=null){            
            panelSkin = GameObject.Find("PanelSkin");
            panelSkin.SetActive(false);
        }
        yield return new WaitForSecondsRealtime(3);
            timer = 0;
        if(PlayerPrefs.GetString("Tutorial") == "tutorialON" && tutorial != null){
            panelLoading.SetActive(false);
            touchControllers.SetActive(false);
            tutorial.gameObject.SetActive(true);
            Time.timeScale = 0;
        }else{
            LevelState(GameManager.State.PLAY);
        }
    }

    void Update()
    {                              
        ResistenceController();
        timer += Time.deltaTime;
        DisplayTime(timer); 
        BonusText();
    }


    public void ButtonPause(){
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        LevelState(GameManager.State.PAUSE);
    }

    public void ButtonNext(){  
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        gm.LoadScene(gm.activeScene + 1, 1);
    }

    public void ButtonBack(){
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        LevelState(GameManager.State.PLAY);
    }

    public void ButtonMenuLevels(){
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        Time.timeScale = 1;
        gm.LoadScene(0, 1);
    }

    public void ButtonRestart(){
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        gm.LoadScene(gm.activeScene, 1);
    }

    public void ButtonBonus1(){
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);
        int x = PlayerPrefs.GetInt("Bonus1");
        if(useBonus & x>0){
            useBonus = false;
            PlayerPrefs.SetInt("Bonus1", x-1);
            Vector2 pos = playerController.transform.position;
            pos.x += 1;
            pos.y += 1;
            Instantiate(bonus[0], pos, Quaternion.identity);
        }
    }

    public void ButtonBonus2(){      
        sourceEffectsMenu.PlayOneShot(clipClickButtonMenu);  
        int x = PlayerPrefs.GetInt("Bonus2");
        if(useBonus & x>0){
            useBonus = false;
            PlayerPrefs.SetInt("Bonus2", x-1);
            Vector2 pos = playerController.transform.position;
            pos.y += 1;
            pos.x += 1;
            Instantiate(bonus[1], pos, Quaternion.identity);   
        }
    }

    public void ButtonJump(){
        playerController.Jump();
    }
    public void ButtomDrop(){
        playerController.DropRock();
    }

    void ResistenceController(){
        sliderBalance.interactable = true;
        sliderBalance.value = playerController.balance;
    }

    void TextLevel(){
        textLevel.text = "" + gm.currentScene;
    }
    void DisplayTime(float timer){
        float minutes = Mathf.FloorToInt(timer / 60);  
        float seconds = Mathf.FloorToInt(timer % 60);
        
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void DisplayTimers(){     
        float timer = 2*timeDiamond;
        float timer1 = timeDiamond;
        float minutes = Mathf.FloorToInt(timer / 60);  
        float seconds = Mathf.FloorToInt(timer % 60);
        float minutes1 = Mathf.FloorToInt(timer1 / 60);  
        float seconds1 = Mathf.FloorToInt(timer1 % 60);
        textTimerFinish.text = textTimer.text;
        textTimer0.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        textTimer1.text = string.Format("{0:00}:{1:00}", minutes1, seconds1);
    }
    public void DiamondsSystem(){
           if(timer>=2*timeDiamond){
                gm.diamondsLevel = 1;
                diamondsSprites[0].SetActive(true);
                diamondsSprites[1].SetActive(false);            
                diamondsSprites[2].SetActive(false); 
            }
            if(timer<2*timeDiamond & timer>timeDiamond){
                gm.diamondsLevel = 2;
                diamondsSprites[0].SetActive(true);
                diamondsSprites[1].SetActive(true);            
                diamondsSprites[2].SetActive(false);            
            }
            if(timer<=timeDiamond){
                gm.diamondsLevel = 3;
                diamondsSprites[0].SetActive(true);
                diamondsSprites[1].SetActive(true);            
                diamondsSprites[2].SetActive(true);            
            }       
    }

    public void LevelState(GameManager.State newState){
        switch (newState){            
            case GameManager.State.LOADING:
                panelLoading.SetActive(true);
                buttonPause.GetComponent<Button>().interactable = false;
                buttonsBonus[0].GetComponent<Button>().interactable = false;
                buttonsBonus[1].GetComponent<Button>().interactable = false;
                break;
            case GameManager.State.PLAY:
                panelPlay.SetActive(true);
                panelLoading.SetActive(false);
                panelPauseFinish.SetActive(false);
                touchControllers.SetActive(true);
                buttonPause.GetComponent<Button>().interactable = true;
                buttonsBonus[0].GetComponent<Button>().interactable = true;
                buttonsBonus[1].GetComponent<Button>().interactable = true;
                Time.timeScale = 1;
                break;
            case GameManager.State.PAUSE:
                panelPauseFinish.SetActive(true);
                touchControllers.SetActive(false);
                Time.timeScale = 0;
                buttonNext.SetActive(false);
                buttonBack.SetActive(true);
                switch(PlayerPrefs.GetInt("valueLanguage")){
                    case 0:
                        textLevelState.text = "PAUSE";
                        break;
                    case 1:
                        textLevelState.text = "PAUSA";
                        break;
                }
                DisplayTimers();
                buttonPause.GetComponent<Button>().interactable = false;
                buttonsBonus[0].GetComponent<Button>().interactable = false;
                buttonsBonus[1].GetComponent<Button>().interactable = false;
                break;
            case GameManager.State.FINISH:
                gm.sourceMusic.volume = 0;
                gm.sourceEffects.volume = 0;
                sourceEffectsMenu.PlayOneShot(clipFinishLvl);
                if(!bonusReceived && panelBonus!=null){
                    StartCoroutine("CourReward");
                }else{
                    LevelState(GameManager.State.LEVELCOMPLETED);
                }
                break;
            case GameManager.State.LEVELCOMPLETED:
                gm.sourceMusic.volume = 0;
                gm.sourceEffects.volume = 0;
                DiamondsSystem();
                gm.DiamondsValue();
                panelPauseFinish.SetActive(true);
                touchControllers.SetActive(false);
                buttonNext.SetActive(true);
                buttonPause.GetComponent<Button>().interactable = false;
                buttonBack.SetActive(false);
                DisplayTimers();
                switch(PlayerPrefs.GetInt("valueLanguage")){
                    case 0:
                        textLevelState.text = "LEVEL COMPLETED";
                        break;
                    case 1:
                        textLevelState.text = "FASE COMPLETA";
                        break;
                }
                buttonNext.GetComponent<Button>().interactable = true;
                buttonsBonus[0].GetComponent<Button>().interactable = false;
                buttonsBonus[1].GetComponent<Button>().interactable = false;
                Time.timeScale = 0;
                break;
            case GameManager.State.GAMEOVER:
                gm.sourceMusic.volume = 0;
                gm.sourceEffects.volume = 0;
                sourceEffectsMenu.PlayOneShot(clipGameOverLvl);            
                panelPauseFinish.SetActive(true);
                touchControllers.SetActive(false);
                buttonPause.GetComponent<Button>().interactable = false;
                buttonNext.SetActive(true);
                buttonBack.SetActive(false);
                DisplayTimers();
                switch(PlayerPrefs.GetInt("valueLanguage")){
                    case 0:
                        textLevelState.text = "GAME   OVER";
                        break;
                    case 1:
                        textLevelState.text = "FIM DE JOGO";
                        break;
                }
                buttonNext.GetComponent<Button>().interactable = false;
                buttonsBonus[0].GetComponent<Button>().interactable = false;
                buttonsBonus[1].GetComponent<Button>().interactable = false;
                Time.timeScale = 0;
                break;
        }
    }

    IEnumerator CourReward(){
        yield return new WaitForSecondsRealtime(1);
        bonusReceived = true;
        panelBonus.SetActive(true);
        textBonusAmount.text = "x" + bonusAmount.ToString();
        PlayerPrefs.SetInt("Bonus" + bonusNumber, bonusAmount);
        yield return new WaitForSecondsRealtime(3);
        panelBonus.SetActive(false);
        LevelState(GameManager.State.LEVELCOMPLETED);
    }
    void BonusText(){
        for (int i = 0; i < textBonusPause.Length; i++){
            textBonusPause[i].text = "x" + PlayerPrefs.GetInt("Bonus" + (i+1)).ToString();            
        }
        for (int i = 0; i < textBonusPlay.Length; i++){  
            textBonusPlay[i].text = "x" + PlayerPrefs.GetInt("Bonus" + (i+1)).ToString();            
        }
    }

    public void NotificationNewReward(string message){
        gm.sourceMusic.Pause();
        sourceEffectsMenu.PlayOneShot(clipTakeSkin);
        Time.timeScale = 0;
        panelSkin.SetActive(true);
        textSkin.text = message;
        touchControllers.SetActive(false);
        StartCoroutine(StopNotification());
    }
    IEnumerator StopNotification(){
        yield return new WaitForSecondsRealtime(3);
        gm.sourceMusic.UnPause();
        panelSkin.SetActive(false);
        touchControllers.SetActive(true);
        Time.timeScale = 1;
    }

}
