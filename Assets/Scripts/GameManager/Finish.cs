using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameManager gm;
    CanvasPlayerController canvasPlayer;
    AdManager adManager;
    // public int vitoriasParaAnuncio;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        canvasPlayer = FindObjectOfType<CanvasPlayerController>();
        adManager = FindObjectOfType<AdManager>();
        gm.vitorias = PlayerPrefs.GetInt("vitorias");
    }
    void Update()
    {
    //    vitoria = PlayerPrefs.GetFloat("vitorias");
    //    vitoriasParaAnuncio = PlayerPrefs.GetInt("vitoriasParaAnuncio");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasPlayer.LevelState(GameManager.State.FINISH);
            gm.DiamondsValue();
            PlayerPrefs.SetInt("ScenesPassed", gm.activeScene);
            GameObject rocksPlayer0 = GameObject.FindGameObjectWithTag("RockController 1");
            GameObject rocksPlayer1 = GameObject.FindGameObjectWithTag("RockController 2");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(rocksPlayer0);
            Destroy(rocksPlayer1);
            Destroy(player);
            Destroy(other.gameObject);
            
            // PlayerPrefs.SetFloat("vitorias", PlayerPrefs.GetFloat("vitorias")+1f);

            if(PlayerPrefs.GetInt("PlayAgain" + gm.activeScene.ToString()) != 1)
            {
                Debug.Log("Novo level");
                PlayerPrefs.SetInt("LvlsWon", gm.activeScene + 1);  // Salva o valor currentScene em PPLvlsWon para saber a fase em que o jogador chegou
            }
            
            if(PlayerPrefs.GetInt("vitorias")>0)
            {
                PlayerPrefs.SetInt("vitorias", 0);
                adManager.ShowInterstitialAd();
            }else{
                PlayerPrefs.SetInt("vitorias", PlayerPrefs.GetInt("vitorias")+1);
                Debug.Log("vitorias: " + (PlayerPrefs.GetInt("vitorias")));
            }

            // if(PlayerPrefs.GetFloat("vitorias")==PlayerPrefs.GetInt("vitoriasParaAnuncio"))
            // {
            //     adManager.ShowInterstitialAd();
            //     PlayerPrefs.SetInt("vitoriasParaAnuncio", PlayerPrefs.GetInt("vitoriasParaAnuncio")+2);
            // }

            

            PlayerPrefs.SetInt("PlayAgain" + gm.activeScene.ToString(), 1);
            
            // if(rocksPlayer0!= null & rocksPlayer1!=null){             
            //}
        }
    }

}
