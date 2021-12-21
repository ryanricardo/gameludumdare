using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameManager gm;
    CanvasPlayerController canvasPlayer;
    AdManager adManager;
    public float vitoria;
    public int vitoriasParaAnuncio;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        canvasPlayer = FindObjectOfType<CanvasPlayerController>();
        adManager = FindObjectOfType<AdManager>();
    }
    void Update()
    {
       vitoria = PlayerPrefs.GetFloat("vitorias");

       vitoriasParaAnuncio = PlayerPrefs.GetInt("vitoriasParaAnuncio");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasPlayer.LevelState(GameManager.State.FINISH);
            gm.DiamondsValue();
            PlayerPrefs.SetInt("ScenesPassed", gm.currentScene);
            GameObject rocksPlayer0 = GameObject.FindGameObjectWithTag("RockController 1");
            GameObject rocksPlayer1 = GameObject.FindGameObjectWithTag("RockController 2");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(rocksPlayer0);
            Destroy(rocksPlayer1);
            Destroy(player);
            Destroy(other.gameObject);
            PlayerPrefs.SetFloat("vitorias", PlayerPrefs.GetFloat("vitorias")+0.5f);

            if(PlayerPrefs.GetInt("PlayAgain" + gm.currentScene.ToString()) != 1)
            {
                Debug.Log("Novo level");
                PlayerPrefs.SetInt("LvlsWon", gm.currentScene + 1);  // Salva o valor currentScene em PPLvlsWon para saber a fase em que o jogador chegou
            }
            

            if(PlayerPrefs.GetFloat("vitorias")==PlayerPrefs.GetInt("vitoriasParaAnuncio"))
            {
                adManager.ShowInterstitialAd();
                PlayerPrefs.SetInt("vitoriasParaAnuncio", PlayerPrefs.GetInt("vitoriasParaAnuncio")+2);
            }
        
            PlayerPrefs.SetInt("PlayAgain" + gm.currentScene.ToString(), 1);
            // if(rocksPlayer0!= null & rocksPlayer1!=null){             
            //}
        }
    }

}
