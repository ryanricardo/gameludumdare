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
    [HideInInspector]private Vector2 offSet;



    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();

        if(startIntoTheGroup)
        {
            EnterGroup();
        }

        offSet = new Vector3(transform.position.x - playerController.transform.position.x, 
        transform.position.y);
    }

    void Update()
    {
        if(typeRock == TypeRock.Follow)
        {
            Vector2 posPlayer = new Vector2(playerController.transform.position.x,
            transform.position.y);

            transform.position = Vector3.Lerp(transform.position, 
            posPlayer, Time.deltaTime * 50);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && typeRock == TypeRock.Idle)
        {
            EnterGroup();
        }

        if(other.gameObject.CompareTag("Plataform") && 
        typeRock == TypeRock.Follow)
        {
            playerController.PushCollisionRocks();
        }

        if(other.gameObject.CompareTag("Plataform Push") &&
        typeRock == TypeRock.Follow)
        {
           if(playerController.isRight)
           {
               LeftGroup(Vector2.left);
           }else 
           {
               LeftGroup(Vector2.right);
           }
        }
    }

    public void EnterGroup()
    {
        
        transform.localScale = new Vector2(playerController.transform.localScale.x, transform.localScale.y);

        gameObject.transform.SetParent(playerController.transform);
        playerController.balance = playerController.balance < 50 ? 
        playerController.balance += 50 : playerController.balance += 0;
        
        transform.position = new Vector2(
        playerController.rocks[playerController.rocks.Count - 1].transform.position.x,
        playerController.rocks[playerController.rocks.Count - 1].transform.position.y + 0.46f);
        
        sourceEffects.PlayOneShot(soundTeletransport);
        rb2.simulated = false;
        playerController.rocks.Add(gameObject);
        StartCoroutine(ChronometerForFollowingPlayer());
    }

    public void LeftGroup(Vector2 vector)
    {
        gameObject.transform.SetParent(null);
        rb2.simulated = true;
        playerController.rocks.Remove(gameObject);
        rb2.AddForce(vector * 4, ForceMode2D.Impulse);
        typeRock = TypeRock.Idle;
    }


    IEnumerator ChronometerForFollowingPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        rb2.simulated = true;
        typeRock = TypeRock.Follow;
    }
}
