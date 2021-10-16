using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject             panelGameOver,      panelLevelCompleted;  
    [HideInInspector]private NewPlayerController    playerController;

    [Header("Atributtes Manager")]
    [HideInInspector]public bool    gameOver = false, levelCompleted = false;
    [HideInInspector]public int     currentScene;
    


    void Start()
    {
        playerController = FindObjectOfType<NewPlayerController>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    void Update()
    {        
        if (levelCompleted){
            panelLevelCompleted.SetActive(true);
            Time.timeScale = 0;
            LoadScene(currentScene + 1, 1.5f);
        }

        if (gameOver){
            panelGameOver.SetActive(true);
            Time.timeScale = 0;
            if (Input.anyKeyDown){
                gameOver = false;
                LoadScene(currentScene, 1);
            }
        }

        if(playerController.balance <= 0)
        {
            gameOver = true;
        }

    }

    IEnumerator SceneDelay(int SceneNumber, float delay){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(SceneNumber);
    }

    public void LoadScene(int SceneNumber, float delay = 0){
        StartCoroutine(SceneDelay(SceneNumber, delay));
    }
}
