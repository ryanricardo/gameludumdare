using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasPlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]private PlayerController playerController;
    [SerializeField]private Slider[]         sliderResistance;
    [SerializeField]private GameManager      gameManager;
    [SerializeField]private TextMeshProUGUI  textTimer;
    [Header("Atributtes Timer")]
    [SerializeField]private float            timer;
    [SerializeField]private float            timeFinished;
    float initialTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.gameOver = false;
        initialTime = timer;
    }

    void Update()
    {
        
        ResistenceController();
        timer -= Time.deltaTime;
        DisplayTime(timer);
        if(timer <= timeFinished)
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
        sliderResistance[0].interactable = true;
        sliderResistance[0].value = playerController.rocksResistances[0] +
        playerController.rocksResistances[1]; 

    }

    void DisplayTime(float timer){
        float minutes = Mathf.FloorToInt(timer / 60);  
        float seconds = Mathf.FloorToInt(timer % 60);
        
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
