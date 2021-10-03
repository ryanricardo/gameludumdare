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
            gm.levelCompleted = true;
            GameObject rocksPlayer0 = GameObject.FindGameObjectWithTag("PedraController 0");
            GameObject rocksPlayer1 = GameObject.FindGameObjectWithTag("PedraController 1");            
            Destroy(rocksPlayer0);
            Destroy(rocksPlayer1);
            Destroy(other.gameObject);
        }
    }
}
