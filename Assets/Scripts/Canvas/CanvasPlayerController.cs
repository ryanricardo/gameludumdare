using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasPlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private Slider              sliderBalance;
    [SerializeField]    private TextMeshProUGUI     textTimer;
    [SerializeField]    private TextMeshProUGUI     textNotification;
    [SerializeField]    private GameObject          gameObjectImageReward;
    [HideInInspector]   private GameManager         gameManager;
    [HideInInspector]   private NewPlayerController playerController;
    
    private bool notification;

    [Header("Atributtes Timer")]
    [SerializeField]    private float               timer;
    [HideInInspector]   private float               initialTime;

    [Header("Atributtes Menus")]
    [HideInInspector]   private bool                menuOpen;
    

    void Start()
    {
        playerController = FindObjectOfType<NewPlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.gameOver = false;
        initialTime = timer;
    }

    void Update()
    {
        
        ResistenceController();
        timer -= Time.deltaTime;
        DisplayTime(timer);
        if(timer <= 0)
        {
            gameManager.gameOver = true;
        }
        if(timer<0){
            timer = 0;
        }
        if(timer<.35f*initialTime){
            textTimer.color = Color.red;
        }

        
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
