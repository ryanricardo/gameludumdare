using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRockController : MonoBehaviour
{
        enum TypeRock
    {
        Idle,
        Follow,
    }

    NewPlayerController playerController;
    Rigidbody2D      rb2;
    [SerializeField]TypeRock         typeRock;
    [SerializeField]AudioSource      sourceEffects;
    [SerializeField]AudioClip        soundTeletransport;
    [SerializeField]private bool     startIntoTheGroup;



    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();

        if(startIntoTheGroup)
        {
            EnterGroup();
        }
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && typeRock == TypeRock.Idle)
        {
            EnterGroup();
        }
    }

    public void EnterGroup()
    {
        
        gameObject.transform.SetParent(playerController.transform);
        playerController.balance = playerController.balance < 50 ? 
        playerController.balance += 50 : playerController.balance += 0;
        
        transform.position = new Vector2(
        playerController.rocks[playerController.rocks.Count - 1].transform.position.x,
        playerController.rocks[playerController.rocks.Count - 1].transform.position.y + 0.4f);
        
        sourceEffects.PlayOneShot(soundTeletransport);
        rb2.simulated = false;
        playerController.rocks.Add(gameObject);
        typeRock = TypeRock.Follow;
    }

    public void LeftGroup()
    {
        gameObject.transform.SetParent(null);
        rb2.simulated = true;
        playerController.rocks.Remove(gameObject);
        rb2.AddForce(Vector2.right * 4, ForceMode2D.Impulse);
        StartCoroutine(ChronometerChangeTypeRock());
    }

    IEnumerator ChronometerChangeTypeRock()
    {
        yield return new WaitForSeconds(1);
        typeRock = TypeRock.Idle;
    }
}

