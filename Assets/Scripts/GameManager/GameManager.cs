using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public  AudioSource            sourceMusic;
    [SerializeField] public  AudioSource            sourceEffects;
    [SerializeField] private AudioClip[]            musicsLofi;              
    [HideInInspector]private NewPlayerController    playerController;
    [HideInInspector]private AdManager              adManager;
    [HideInInspector]private InventoryManager       inventoryManager;
    [HideInInspector]private Data                   data;
    [HideInInspector]private CanvasPlayerController canvasPlayer;

    [Header("Atributtes Manager")]
    public int nivel;
    public int currentScene, lvlsNivel, activeScene, diamondsNivel, diamondsLevel, mortes;
    public int mortesParaAnuncio;
    private bool useOneTime;

    State levelState;
    public enum State {LOADING, PLAY, PAUSE, FINISH, LEVELCOMPLETED, GAMEOVER};

    void Awake()
    {
        useOneTime = true;
        Screen.orientation = ScreenOrientation.Landscape;
        activeScene = SceneManager.GetActiveScene().buildIndex;
        data = FindObjectOfType<Data>();
        canvasPlayer = FindObjectOfType<CanvasPlayerController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerController = FindObjectOfType<NewPlayerController>();
        adManager = FindObjectOfType<AdManager>();
        sourceMusic = GetComponent<AudioSource>();
        sourceEffects = playerController.GetComponent<AudioSource>();

    }

    void Start()
    {
        Time.timeScale = 1;                
        lvlsNivel = 20;
        sourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
        sourceEffects.volume = PlayerPrefs.GetFloat("VolumeEffectsGame");
        sourceMusic.volume = PlayerPrefs.GetFloat("VolumeMusicGame");
        sourceMusic.clip = musicsLofi[Random.Range(1, musicsLofi.Length)];
        sourceMusic.Play();

        data.rocks[1].GetComponent<SpriteRenderer>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        data.rocks[2].GetComponent<SpriteRenderer>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
        playerController.GetComponent<SpriteRenderer>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];
    }

    void Update()
    {        
           mortes=PlayerPrefs.GetInt("mortes");
           mortesParaAnuncio=PlayerPrefs.GetInt("mortesParaAnuncio");

        // if(playerController.getKeyDownR)
        // {
        //     LoadScene(currentScene, 1.5f);
        //     imageRestart.gameObject.SetActive(true);
        // }

        if(PlayerPrefs.GetInt("mortesParaAnuncio")==PlayerPrefs.GetInt("mortes"))
        {
            PlayerPrefs.SetInt("mortesParaAnuncio", PlayerPrefs.GetInt("mortesParaAnuncio")+3);
           adManager.ShowInterstitialAd();
        }

        if(playerController.balance <= 0)
        {
            canvasPlayer.LevelState(State.GAMEOVER);
            if(useOneTime)
            {
                PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortes")+1);
                useOneTime = false;
            }
            Debug.Log(PlayerPrefs.GetInt("mortes"));
            playerController.balance = 1;
        }

        if(!sourceMusic.isPlaying)
        {
            sourceMusic.clip = musicsLofi[Random.Range(1, musicsLofi.Length)];
            sourceMusic.Play();
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
        Debug.Log("Valor de DiamondsLvl" + currentScene + ": " + PlayerPrefs.GetInt(PPlvl));
        Debug.Log("E valor de diamondsLevel é: " + diamondsLevel);

        if(PlayerPrefs.GetInt(PPlvl)<diamondsLevel){
            Debug.Log("Salvo diamantes na cena: " + currentScene);
            PlayerPrefs.SetInt(PPlvl, diamondsLevel);   // Salva o valor de diamantes na fase      
            PlayerPrefs.SetInt("Diamonds" + nivel, PlayerPrefs.GetInt(PPlvl) + PlayerPrefs.GetInt("Diamonds" + nivel));
        }
    }
    
}
