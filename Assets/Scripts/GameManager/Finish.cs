using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameManager gm;
    CanvasPlayerController canvasPlayer;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        canvasPlayer = FindObjectOfType<CanvasPlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("RockController 1")
        || other.gameObject.CompareTag("RockController 2"))
        {
            PlayerPrefs.SetInt("ScenesPassed", gm.currentScene);
            GameObject rocksPlayer0 = GameObject.FindGameObjectWithTag("RockController 1");
            GameObject rocksPlayer1 = GameObject.FindGameObjectWithTag("RockController 2");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Destroy(rocksPlayer0);
            Destroy(rocksPlayer1);
            Destroy(player);
            Destroy(other.gameObject);
            
            PlayerPrefs.SetInt("LvlsWon", gm.currentScene + 1);  // Salva o valor currentScene em PPLvlsWon para saber a fase em que o jogador chegou
            gm.DiamondsValue();
            canvasPlayer.LevelState(GameManager.State.FINISH);

            // if(rocksPlayer0!= null & rocksPlayer1!=null){             
            //}
        }
    }

}
