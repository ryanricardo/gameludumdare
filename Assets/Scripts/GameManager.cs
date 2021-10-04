using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panelGameOver;    
    [SerializeField]public bool gameOver = false, levelCompleted = false;
    [SerializeField]int currentScene;
    [SerializeField]private PlayerController playerController;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    void Update()
    {        
        if (levelCompleted){
            // panelLevelCompleted.SetActive(true);
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

        if(playerController.rocksResistancesEnd[0] && playerController.rocksResistancesEnd[1])
        {
            gameOver = true;
        }

        if (Input.GetButtonDown("Cancel")){
            LoadScene(0,0);
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
