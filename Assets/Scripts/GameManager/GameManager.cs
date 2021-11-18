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
    [SerializeField] private AudioClip[]            musicsLofi;              
    [HideInInspector]private NewPlayerController    playerController;
    [HideInInspector]private InventoryManager       inventoryManager;
    [HideInInspector]private Data                   data;
    [HideInInspector]private CanvasPlayerController canvasPlayer;

    [Header("Atributtes Manager")]
    public int nivel;
    public int currentScene, lvlsNivel, diamondsNivel, diamondsLevel;

    State levelState;
    public enum State {PLAY, PAUSE, FINISH, LEVELCOMPLETED, GAMEOVER};

    void Awake()
    {
        souceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame") * PlayerPrefs.GetFloat("VolumeGeneral");
        souceEffects.volume = PlayerPrefs.GetFloat("VolumeEffectsGame") * PlayerPrefs.GetFloat("VolumeGeneral");
        data = FindObjectOfType<Data>();
        canvasPlayer = FindObjectOfType<CanvasPlayerController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerController = FindObjectOfType<NewPlayerController>();
        playerController.GetComponent<SpriteRenderer>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];
        currentScene = SceneManager.GetActiveScene().buildIndex;
        lvlsNivel = 16;

        souceMusic.clip = musicsLofi[Random.Range(1, musicsLofi.Length)];
        souceMusic.Play();

    }

    void Start()
    {
        // imageRestart.gameObject.SetActive(false);
        Time.timeScale = 1;
        souceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
        Debug.Log(PlayerPrefs.GetInt("SkinRock1"));
        Debug.Log(PlayerPrefs.GetInt("SkinRock2"));
        data.rocks[1].GetComponent<SpriteRenderer>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        data.rocks[2].GetComponent<SpriteRenderer>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
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
            canvasPlayer.LevelState(State.GAMEOVER);
        }

        if(!souceMusic.isPlaying)
        {
            souceMusic.clip = musicsLofi[Random.Range(1, musicsLofi.Length)];
            souceMusic.Play();
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
    
    public void DiamondsValue(){
        string PPlvl = "DiamondsLvl" + currentScene;
        if(PlayerPrefs.GetInt(PPlvl)<diamondsLevel){
            PlayerPrefs.SetInt(PPlvl, diamondsLevel);   // Salva o valor de diamantes na fase      
            PlayerPrefs.SetInt("Diamonds" + nivel, PlayerPrefs.GetInt(PPlvl) + PlayerPrefs.GetInt("Diamonds" + nivel));
        }
    }
    
}
