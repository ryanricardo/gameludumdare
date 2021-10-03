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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag){
            case "Player":            
            gm.levelCompleted = true;
            Destroy(other.gameObject);
            break;
        }
    }
}
