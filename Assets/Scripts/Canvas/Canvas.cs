using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{

    [Header("Components")]
    [HideInInspector]   private NewPlayerController     playerController;
    [HideInInspector]   private GameManager             gameManager;
    [SerializeField]    private GameObject              imageMenu;

    [Header("Atributtes Canvas")]
    [HideInInspector]   private bool                    menuOpen;
    
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<NewPlayerController>();
        menuOpen = true;
    }

    void Update()
    {
        
        if(playerController.getKeyDownEsc)
        {
            menuOpen ^= true;
            if(menuOpen)
            {
                imageMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }else 
            {
                imageMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            
        }
    }

    public void ReniciarFase()
    {
        gameManager.LoadScene(gameManager.currentScene, 1);
    }

    public void BackMenu()
    {
        gameManager.LoadScene(0, 1);
    }
}
