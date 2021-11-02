using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject             panelGameOver,      panelLevelCompleted;  
    [SerializeField] private GameObject             imageRestart;   
    [HideInInspector]private NewPlayerController    playerController;
    [HideInInspector]private InventoryManager       inventoryManager;
    [HideInInspector]private Data                   data;

    [Header("Atributtes Manager")]
    [HideInInspector]public bool    gameOver = false, levelCompleted = false;
    [HideInInspector]public int     currentScene;


    


    void Start()
    {
        data = FindObjectOfType<Data>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerController = FindObjectOfType<NewPlayerController>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        imageRestart.gameObject.SetActive(false);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("PPLvlsWon", currentScene);      // Salva o valor currentScene em PPLvlsWon para saber a fase em que o jogador chegou 
        data.rocks[1].GetComponent<SpriteRenderer>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        Debug.Log(PlayerPrefs.GetInt("SkinRock1"));
    }

    void Update()
    {        
        if (levelCompleted){
            panelLevelCompleted.SetActive(true);
            Time.timeScale = 0;
            LoadScene(currentScene + 1, 1.5f);
        }

        if(playerController.getKeyDownR)
        {
            LoadScene(currentScene, 1.5f);
            imageRestart.gameObject.SetActive(true);
        }

        if (gameOver){
            panelGameOver.SetActive(true);
            StartCoroutine(StopTimeDelay());
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

    IEnumerator StopTimeDelay()
    {
        yield return new WaitForSeconds(0.0001f);
        Time.timeScale = Time.timeScale > 0.1f ? Time.timeScale -= 0.017f : Time.timeScale = 0;
    }

    public void LoadScene(int SceneNumber, float delay = 0){
        StartCoroutine(SceneDelay(SceneNumber, delay));
    }
}
