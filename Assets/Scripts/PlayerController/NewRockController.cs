using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRockController : MonoBehaviour
{
    public enum CategoryRock
    {
        controller,
        pickup,
    }

    public enum TypeRock
    {
        rock1,
        rock2,
    }

    [Header("Components")]
    [SerializeField]    public  CategoryRock        categoryRock;
    [SerializeField]    public  TypeRock            typeRock;
    [HideInInspector]   private NewPlayerController playerController;
    [HideInInspector]   private Rigidbody2D         rb2;
    [HideInInspector]   private Vector2             offSet;

    [Header("Atributtes Rocks")]
    [SerializeField]    private float               forcePushPickup;
    [SerializeField]    private float               speedMoveNewPosition;
    [SerializeField]    private float               followDelay;
    [SerializeField]    private bool                pushOneTime;
    [SerializeField]    private bool                moveNewPosition; 

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();
        //offSet = transform.position - playerController.transform.position;
        offSet = new Vector3(transform.position.x - playerController.transform.position.x, 
        transform.position.y);
    }

    void Update()
    {
        /* Aqui, é o modo em que cada rock controller esta no momento
        em controller ele mantem o movimento junto do player
        em pickup, ele esta fora do player. */

        switch(categoryRock)
        {
            case CategoryRock.controller:
                ControllerPush();
                Movimentation();
            break;

            case CategoryRock.pickup:
                PushThisPickup();
            break;
        }
        
    }

    void ControllerPush()
    {
        /* Como as duas pedras controller usam o mesmo script foi necessario usar um enum
        para identificar em cena qual objeto é qual */

        switch(typeRock)
        {
            case TypeRock.rock1:
                if(playerController.balance <= 50)
                {
                    categoryRock = CategoryRock.pickup;
                }
            break;
            case TypeRock.rock2:
                if(playerController.balance <= 0)
                {
                    categoryRock = CategoryRock.pickup;
                }
            break;
        }
    }

    void Movimentation()
    {

        /* Aqui de fato é feita a movimentação da pedra.
        em um primeira estado ele deve ir a posição do player
        e após chegar na posição do player, deve segui-lo. */

        if(moveNewPosition)
        {
            Vector3 newPos = new Vector3(playerController.transform.position.x,
            playerController.transform.position.y + 2, transform.position.z);

            transform.position = Vector2.MoveTowards(transform.position, newPos, 
            speedMoveNewPosition);

            if(transform.position.y == newPos.y)
            {
                categoryRock = CategoryRock.controller;
                moveNewPosition = false;
            }
        }else 
        {

            Vector2 posPlayer = new Vector2(playerController.transform.position.x + offSet.x,
            transform.position.y);

            transform.position = Vector3.Lerp(transform.position, 
            posPlayer, Time.deltaTime * followDelay);
        }   
    }

    void PushThisPickup()
    {

        /* Metodo simples para empurra-lo para frente quando o outro metodo
        verificar que acabou um equilibrio. */
        if(!pushOneTime)
        {
            if(playerController.dropRock)
            {
                if(playerController.isRight)
                {
                    rb2.AddForce(Vector2.right * forcePushPickup, ForceMode2D.Impulse);
                }else if(!playerController.isRight)
                {
                    rb2.AddForce(Vector2.left * forcePushPickup, ForceMode2D.Impulse);
                }
                playerController.dropRock = false;
            }else 
            {
                if(playerController.isRight)
                {
                    rb2.AddForce(Vector2.left * forcePushPickup, ForceMode2D.Impulse);
                }else if(!playerController.isRight)
                {
                     
                    rb2.AddForce(Vector2.right * forcePushPickup, ForceMode2D.Impulse);
                }
            }

            pushOneTime = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && categoryRock == CategoryRock.pickup)
        {
            moveNewPosition = true;
            pushOneTime = false;
            if(playerController.balance < 30)
            {
                playerController.balance += (100 - playerController.balance) / 1.3f;
            }else 
            {
                playerController.balance += (100 - playerController.balance) / 2;
            }

            categoryRock = CategoryRock.controller;
        }

        if(other.gameObject.CompareTag("Plataform") && categoryRock == CategoryRock.controller)
        {
            categoryRock = CategoryRock.pickup;
        }
    }
}
