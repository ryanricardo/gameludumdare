using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{

    private PlayerController playerController;
    private bool             menuOpen;
    [SerializeField]private GameObject imageMenu;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
        menuOpen = true;
    }

    void Update()
    {
        
        /*if(playerController.getKeyDownEsc)
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
            
        }*/
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
