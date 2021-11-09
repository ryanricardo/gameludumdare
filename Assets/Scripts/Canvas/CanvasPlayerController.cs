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
    [SerializeField]    private TextMeshProUGUI     textTimer, textNotification, textLevelState;
    [SerializeField]    private GameObject          gameObjectImageReward, panelPlay, panelLevel, buttonNext, buttonBack;
    [HideInInspector]   private GameManager         gm;
    [HideInInspector]   private NewPlayerController playerController;
    
    private bool notification;

    [Header("Atributtes Timer")]
    [SerializeField]    private float               timer;
    [SerializeField]    private float[]             timeDiamond;

    [Header("Atributtes Menus")]
    [HideInInspector]   private bool                menuOpen;
    

    void Start()
    {
        playerController = FindObjectOfType<NewPlayerController>();
        gm = FindObjectOfType<GameManager>();
        timer = 0;
        gm.levelState = GameManager.State.PLAY;
        buttonNext.GetComponent<Button>().interactable = true;
    }

    void Update()
    {                              
        ResistenceController();
        timer += Time.deltaTime;
        DisplayTime(timer);
        ControllerTimer();     

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
                Time.timeScale = 0;
                break;
            case GameManager.State.LEVELCOMPLETED:
                panelLevel.SetActive(true);
                panelPlay.SetActive(false);
                buttonNext.SetActive(true);
                buttonBack.SetActive(false);
                textLevelState.text = "LEVEL COMPLETED";
                buttonNext.GetComponent<Button>().interactable = true;
                Time.timeScale = 0;
                break;
            case GameManager.State.GAMEOVER:            
                panelLevel.SetActive(true);
                panelPlay.SetActive(false);
                buttonNext.SetActive(true);
                buttonBack.SetActive(false);
                textLevelState.text = "GAME   OVER";
                buttonNext.GetComponent<Button>().interactable = false;
                Time.timeScale = 0;
                break;
        }
            
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
        gm.LoadScene(0, 1.5f);
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
        gameObjectImageReward.gameObject.GetComponent<Image>().sprite 
        = sprite;
        textNotification.text = message;
        StartCoroutine(StopNotification());
    }

    void ControllerTimer(){        
        if(timer<0){
            timer = 0;
        }  
    }

    void DisplayTime(float timer){
        float minutes = Mathf.FloorToInt(timer / 60);  
        float seconds = Mathf.FloorToInt(timer % 60);
        
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator StopNotification()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        gameObjectImageReward.gameObject.SetActive(false);
        textNotification.gameObject.SetActive(false);
        notification = false;
    }


}
