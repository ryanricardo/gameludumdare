using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    // [SerializeField] private GameObject             imageRestart;   
    [SerializeField] private AudioSource            souceMusic;
    [SerializeField] private AudioSource            souceEffects;
    [HideInInspector]private NewPlayerController    playerController;
    [HideInInspector]private InventoryManager       inventoryManager;
    [HideInInspector]private Data                   data;

    [Header("Atributtes Manager")]
    public int currentScene;

    [HideInInspector] public State levelState;
    public enum State {PLAY, PAUSE, LEVELCOMPLETED, GAMEOVER};

    void Awake()
    {
        souceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
        souceEffects.volume = PlayerPrefs.GetFloat("VolumeEffectsGame");
    }

    void Start()
    {
        data = FindObjectOfType<Data>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerController = FindObjectOfType<NewPlayerController>();
        // imageRestart.gameObject.SetActive(false);
        Time.timeScale = 1;
        data.rocks[1].GetComponent<SpriteRenderer>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        data.rocks[2].GetComponent<SpriteRenderer>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
        playerController.GetComponent<SpriteRenderer>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];

        souceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
        Debug.Log(PlayerPrefs.GetInt("SkinRock1"));
    }

    void Update()
    {        
        // if(playerController.getKeyDownR)
        // {
        //     LoadScene(currentScene, 1.5f);
        //     imageRestart.gameObject.SetActive(true);
        // }
        if(playerController.balance <= 0)
        {
            levelState = State.GAMEOVER;
        }

    }

    IEnumerator SceneDelay(int SceneNumber, float delay){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(SceneNumber);
    }

    public IEnumerator StopTimeDelay()
    {
        yield return new WaitForSeconds(0.0001f);
        Time.timeScale = Time.timeScale > 0.1f ? Time.timeScale -= 0.017f : Time.timeScale = 0;
    }

    public void LoadScene(int SceneNumber, float delay = 0){
        StartCoroutine(SceneDelay(SceneNumber, delay));
    }

    
}
