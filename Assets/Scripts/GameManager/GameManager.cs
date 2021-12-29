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
    [SerializeField]private Data                   data;
    [HideInInspector]private CanvasPlayerController canvasPlayer;
    [SerializeField] private GameObject[]           rocks;

    [Header("Atributtes Manager")]
    public int nivel;
    public int currentScene, lvlsNivel, activeScene, diamondsNivel, diamondsLevel, mortes, vitorias;
    // public int mortesParaAnuncio;
    private bool useOneTime;

    State levelState;
    public enum State {LOADING, PLAY, PAUSE, FINISH, LEVELCOMPLETED, GAMEOVER};

    void Awake()
    {
        useOneTime = true;
        Screen.orientation = ScreenOrientation.Landscape;
        activeScene = SceneManager.GetActiveScene().buildIndex;
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

        rocks = new GameObject[4];

        rocks[1] = GameObject.FindGameObjectWithTag("RockController 1");
        rocks[2] = GameObject.FindGameObjectWithTag("RockController 2");
        rocks[3] = GameObject.FindGameObjectWithTag("Player");

        rocks[1].GetComponent<SpriteRenderer>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        rocks[2].GetComponent<SpriteRenderer>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
        rocks[3].GetComponent<SpriteRenderer>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];

        mortes = PlayerPrefs.GetInt("mortes");
    }

    void Update()
    {        
        //    mortes=PlayerPrefs.GetInt("mortes");
        //    mortesParaAnuncio=PlayerPrefs.GetInt("mortesParaAnuncio");

        // if(playerController.getKeyDownR)
        // {
        //     LoadScene(currentScene, 1.5f);
        //     imageRestart.gameObject.SetActive(true);
        // }

        // if(PlayerPrefs.GetInt("mortesParaAnuncio")==PlayerPrefs.GetInt("mortes"){   
        //     PlayerPrefs.SetInt("mortesParaAnuncio", PlayerPrefs.GetInt("mortesParaAnuncio")+3);
        //     adManager.ShowInterstitialAd();
        // }

        if(playerController.balance <= 0) {         

            playerController.balance = 1;
            playerController.source.Stop();

            canvasPlayer.LevelState(State.GAMEOVER);

            // if(useOneTime)
            // {
            //     PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortes")+1);
            //     useOneTime = false;
            // }           


            if (useOneTime){
                if (PlayerPrefs.GetInt("mortes") == 2)
                {
                    useOneTime = true;
                    PlayerPrefs.SetInt("mortes", 0);
                    adManager.ShowInterstitialAd();
                }else if (PlayerPrefs.GetInt("mortes") >= 0 && PlayerPrefs.GetInt("mortes") <= 1)
                {
                    useOneTime = false;
                    PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortes") + 1);
                }
            }
            Debug.Log("mortes: " + (PlayerPrefs.GetInt("mortes")));
        }

        // if(levelState == State.GAMEOVER){
              
            

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
        string PPlvl = "DiamondsLvl" + activeScene;
        Debug.Log("Valor de DiamondsLvl" + activeScene + ": " + PlayerPrefs.GetInt(PPlvl));
        Debug.Log("E valor de diamondsLevel é: " + diamondsLevel);

        if(PlayerPrefs.GetInt(PPlvl)<diamondsLevel){
            Debug.Log("Salvo diamantes na cena: " + activeScene);
            PlayerPrefs.SetInt(PPlvl, diamondsLevel);   // Salva o valor de diamantes na fase      
            PlayerPrefs.SetInt("Diamonds" + nivel, PlayerPrefs.GetInt(PPlvl) + PlayerPrefs.GetInt("Diamonds" + nivel));
        }
    }
    
}
