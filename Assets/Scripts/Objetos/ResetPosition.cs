using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [Header("Components")]
    // NewPlayerController player;
    // GameManager gameManager;
    Vector3 playerPos, rock1Pos, rock2Pos;
    GameObject player, rock1, rock2;

    void Start()
    {
        // gameManager = FindObjectOfType<GameManager>();
        // player = FindObjectOfType<NewPlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        rock1 = GameObject.FindGameObjectWithTag("RockController 1");
        rock2 = GameObject.FindGameObjectWithTag("RockController 2");
        playerPos = player.transform.position;
        rock1Pos = rock1.transform.position;
        rock2Pos = rock2.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player")){
            // gameManager.gameOver = true;
            player.transform.position = playerPos;
            rock1.transform.position = rock1Pos;
            rock2.transform.position = rock2Pos;
        }
        if(other.gameObject.CompareTag("RockController 1")){
            rock1.transform.position = rock1Pos;
        }
    }

}
