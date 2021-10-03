using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panelGameOver;    
    [HideInInspector] public bool gameOver = false, levelCompleted = false;
    int currentScene;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {        
        if (levelCompleted){
            // panelLevelCompleted.SetActive(true);
              LoadScene(currentScene + 1, 2);
        }

        if (gameOver){
            panelGameOver.SetActive(true);
            if (Input.anyKeyDown){
                gameOver = false;
                LoadScene(currentScene, 1);
            }
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
