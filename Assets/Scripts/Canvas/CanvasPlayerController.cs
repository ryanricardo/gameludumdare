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
    [SerializeField]    private TextMeshProUGUI     textNotification, textLevel, textTimer, textTimer0, textTimer1, textTimerFinish;
    [SerializeField]    private GameObject          gameObjectImageReward, panelPlay, panelLevel, buttonNext, buttonBack;
    [SerializeField]    private GameObject[]        diamondsSprites;  
    [HideInInspector]   private GameManager         gm;
    [HideInInspector]   private NewPlayerController playerController;
    [HideInInspector]   private Data data;
    
    private bool notification;
    TextMeshProUGUI textLevelState;

    [Header("Atributtes Timer")]
    [SerializeField]    private float timer;
    [SerializeField]    private float timeDiamond;

    [Header("Atributtes Menus")]
    [HideInInspector]   private bool menuOpen;
    [HideInInspector]   private bool openPanelPause;
    
    private void Awake()
    {        
        playerController = FindObjectOfType<NewPlayerController>();
        gm = FindObjectOfType<GameManager>();
        data = FindObjectOfType<Data>();
        buttonNext.GetComponent<Button>().interactable = true;
        openPanelPause = false;
    }

    void Start()
    {
        gm.levelState = GameManager.State.PLAY;
        timer = 0;
        TextLevel();
        timeDiamond = SceneManager.GetActiveScene().buildIndex * 5 + 25;
        for (int i = 0; i < diamondsSprites.Length; i++){
            diamondsSprites[i].SetActive(false);
        }
    }

    void Update()
    {                              
        ResistenceController();
        timer += Time.deltaTime;
        DisplayTime(timer);    
        DiamondsSystem();   
        LevelState();   
    }

    public void ButtonPause(){
        gm.levelState = GameManager.State.PAUSE;
    }

    public void ButtonNext(){
        gm.LoadScene(gm.currentScene + 1, 1);

    }

    public void ButtonBack(){
        gm.levelState = GameManager.State.PLAY;
    }

    public void ButtonMenuLevels(){
        gm.LoadScene(0, 1);
    }

    public void ButtonRestart(){
        gm.LoadScene(gm.currentScene, 1);
    }

    void ResistenceController()
    {
        sliderBalance.interactable = true;
        sliderBalance.value = playerController.balance;
    }

    public void NotificationNewReward(Sprite sprite, string message)
    {
        Time.timeScale = 0.2f;
        textNotification.gameObject.SetActive(true);
        gameObjectImageReward.gameObject.SetActive(true);
        gameObjectImageReward.gameObject.GetComponent<Image>().sprite = sprite;
        textNotification.text = message;
        StartCoroutine(StopNotification());
    }
    IEnumerator StopNotification()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        gameObjectImageReward.gameObject.SetActive(false);
        textNotification.gameObject.SetActive(false);
        notification = false;
    }

    void TextLevel(){
        switch (gm.nivel){
            case 1:
                textLevel.text = "LEVEL " + gm.currentScene;
                break;
            case 2:
                textLevel.text = "LEVEL " + (gm.currentScene - gm.lvlsNivel);
                break;
            case 3:
                textLevel.text = "LEVEL " + (gm.currentScene - gm.lvlsNivel * 2);
                break;
            case 4:
                textLevel.text = "LEVEL " + (gm.currentScene - gm.lvlsNivel * 3);
                break;
            case 5:
                textLevel.text = "LEVEL " + (gm.currentScene - gm.lvlsNivel * 4);
                break;
            case 6:
                textLevel.text = "LEVEL " + (gm.currentScene - gm.lvlsNivel * 5);
                break;
        }
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

    void DiamondsSystem(){
        if(gm.levelState == GameManager.State.LEVELCOMPLETED){
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
        }else{
            gm.diamondsLevel = 0;
        }        
    }

    void LevelState(){
        switch (gm.levelState){
            case GameManager.State.PLAY:
                panelPlay.SetActive(true);
                panelLevel.SetActive(false);
                Time.timeScale = 1;
                break;
            case GameManager.State.PAUSE:
                panelLevel.SetActive(true);
                panelPlay.SetActive(false);
                buttonNext.SetActive(false);
                buttonBack.SetActive(true);
                textLevelState.text = "PAUSE";
                DisplayTimers();
                Time.timeScale = 0;
                break;
            case GameManager.State.LEVELCOMPLETED:
                panelLevel.SetActive(true);
                panelPlay.SetActive(false);
                buttonNext.SetActive(true);
                buttonBack.SetActive(false);
                DisplayTimers();
                textLevelState.text = "LEVEL COMPLETED";
                PlayerPrefs.SetInt("LvlsWon", gm.currentScene + 1);  // Salva o valor currentScene em PPLvlsWon para saber a fase em que o jogador chegou
                gm.DiamondsValue();
                buttonNext.GetComponent<Button>().interactable = true;
                Time.timeScale = 0;
                break;
            case GameManager.State.GAMEOVER:            
                panelLevel.SetActive(true);
                panelPlay.SetActive(false);
                buttonNext.SetActive(true);
                buttonBack.SetActive(false);
                DisplayTimers();
                textLevelState.text = "GAME   OVER";
                buttonNext.GetComponent<Button>().interactable = false;
                Time.timeScale = 0;
                break;
        }
    }



}
