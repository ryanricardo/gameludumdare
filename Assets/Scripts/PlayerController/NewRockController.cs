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

    [Header("Components")]
    [HideInInspector]   private     Vector2                 offSet;
    [HideInInspector]   private     Rigidbody2D             rb2;
    [HideInInspector]   private     NewPlayerController     playerController;
    [SerializeField]    private     TypeRock                typeRock;
    [SerializeField]    private     AudioSource             sourceEffects;
    [SerializeField]    private     AudioClip               soundTeletransport;

    [Header("Atributtes General")]
    [SerializeField]    private     bool                    startIntoTheGroup;
    [SerializeField]    private     float                   minDistanceForLeftGroup;
    [SerializeField]    private     float                   speedParallaxIntoGroup;
    [SerializeField]    private     float                   forceForLeftGroup;



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
            posPlayer, Time.deltaTime * speedParallaxIntoGroup);
            float distancePlayer = Vector2.Distance(transform.position, playerController.transform.position);
            
            if(distancePlayer >= minDistanceForLeftGroup)
            {
                Debug.Log("Drop");
                typeRock = TypeRock.Idle;
            }
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
           playerController.LeftGroupRocks();
        }

        if(other.gameObject.CompareTag("Button") &&
        typeRock == TypeRock.Idle)
        {
            other.gameObject.GetComponent<BotaocomTimer>().EnterRockButton(gameObject.transform, gameObject);
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
        playerController.rocksOut.Remove(gameObject);
        StartCoroutine(ChronometerForFollowingPlayer());
    }

    public void LeftGroup(Vector2 vector)
    {
        rb2.gravityScale = 1;
        gameObject.transform.SetParent(null);
        rb2.simulated = true;
        playerController.rocks.Remove(gameObject);
        playerController.rocksOut.Add(gameObject);
        rb2.AddForce(vector * forceForLeftGroup, ForceMode2D.Impulse);
        typeRock = TypeRock.Idle;
    }


    IEnumerator ChronometerForFollowingPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        rb2.simulated = true;
        typeRock = TypeRock.Follow;
    }
}
