using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") )
        {            
            PlayerPrefs.SetInt("ScenesPassed", gm.currentScene);
            GameObject rocksPlayer0 = GameObject.FindGameObjectWithTag("RockController 1");
            GameObject rocksPlayer1 = GameObject.FindGameObjectWithTag("RockController 2");  
            gm.levelCompleted = true;   
            Destroy(rocksPlayer0);
            Destroy(rocksPlayer1);
            Destroy(other.gameObject);
           // if(rocksPlayer0!= null & rocksPlayer1!=null){                
                       

            //}
        }
    }
}
