using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetPosition : MonoBehaviour
{
    [Header("Components")]
    // NewPlayerController player;
    // GameManager gameManager;

    public AudioSource sourceReset;
    public AudioClip     soundReset;
    public float resetTime = 1;
    Vector3 playerPos, rock1Pos, rock2Pos;
    GameObject player, rock1, rock2;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
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

        if(!sourceReset.isPlaying)
        {
            gm.sourceMusic.Pause();
            sourceReset.PlayOneShot(soundReset);
        }
        
        if(other.gameObject.CompareTag("Player")){
            // gameManager.gameOver = true;
            StartCoroutine(SpawnPlayers());

        }
        if(other.gameObject.CompareTag("RockController 1")){
            StartCoroutine(SpawnRocks());
            
        }

    }

    

    IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(resetTime);
        gm.sourceMusic.UnPause();
        player.transform.position = playerPos;
        rock1.transform.position = rock1Pos;
        rock2.transform.position = rock2Pos;
    }

    IEnumerator SpawnRocks()
    {
        yield return new WaitForSeconds(resetTime);
        gm.sourceMusic.UnPause();
        rock1.transform.position = rock1Pos;
    }


}
