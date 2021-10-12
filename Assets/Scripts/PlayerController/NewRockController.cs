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
    [Header("Atributtes Rocks")]
    [SerializeField]    private float               forcePushPickup;
    [SerializeField]    private float               speedMoveNewPosition;
    [HideInInspector]   private bool                moveNewPosition; 
    [HideInInspector]   private bool                pushOneTime;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();
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

            if(transform.position == newPos)
            {
                categoryRock = CategoryRock.controller;
                moveNewPosition = false;
            }
        }else 
        {
            transform.position = new Vector2(playerController.transform.position.x, transform.position.y);
        }   
    }

    void PushThisPickup()
    {

        /* Metodo simples para empurra-lo para frente quando o outro metodo
        verificar que acabou um equilibrio. */

        if(!pushOneTime)
        {
            rb2.AddForce(transform.right * forcePushPickup, ForceMode2D.Impulse);


            pushOneTime = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && categoryRock == CategoryRock.pickup)
        {
            Debug.Log("Touch");
            moveNewPosition = true;
            pushOneTime = false;
            Debug.Log("O equilibrio esta em " + playerController.balance);
            if(playerController.balance < 30)
            {
                playerController.balance += (100 - playerController.balance) / 1.3f;
            }else 
            {
                playerController.balance += (100 - playerController.balance) / 2;
            }

            Debug.Log("Agora esta em " + playerController.balance);
            categoryRock = CategoryRock.controller;
        }
    }
}
