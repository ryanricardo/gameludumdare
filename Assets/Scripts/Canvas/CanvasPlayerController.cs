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
    [HideInInspector]   private GameManager         gameManager;
    [HideInInspector]   private NewPlayerController playerController;

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

    void DisplayTime(float timer){
        float minutes = Mathf.FloorToInt(timer / 60);  
        float seconds = Mathf.FloorToInt(timer % 60);
        
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
