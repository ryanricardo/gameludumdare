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

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        ResistenceController();
        timer += Time.deltaTime;
        textTimer.text = Mathf.FloorToInt(timer).ToString();
        if(timer >= timeFinished)
        {
            gameManager.gameOver = true;
        }
        
    }

    void ResistenceController()
    {
       sliderResistance[0].interactable = true;
       if(sliderResistance[0].value > 50) { sliderResistance[0].value = playerController.rocksResistances[0] + 50; }
       else sliderResistance[0].value = playerController.rocksResistances[1];
    }
}
