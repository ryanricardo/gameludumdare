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
    [HideInInspector]   private bool[]              changeLeft;
    [HideInInspector]   private bool[]              changeRight;

    void Start()
    {
        changeLeft = new bool[2];
        changeRight  = new bool[2];
        changeRight[0] = false;
        changeLeft[0] = true;
        changeRight[1] = false;
        changeLeft[1] = true;
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
            if(playerController.dropRock)
            {
                if(playerController.isRight && changeRight[0])
                {
                    forcePushPickup = Mathf.Abs(forcePushPickup);
                    changeLeft[0] = true;
                    changeRight[0] = false;
                }else if(!playerController.isRight && changeLeft[0])
                {
                    forcePushPickup = -forcePushPickup;
                    changeRight[0] = true;
                    changeLeft[0] = false;
                }
                playerController.dropRock = false;
            }else 
            {
                if(playerController.isRight && changeRight[1])
                {
                    forcePushPickup = -forcePushPickup;
                    changeLeft[1] = true;
                    changeRight[1] = false;
                }else if(!playerController.isRight && changeLeft[1])
                {
                    forcePushPickup = Mathf.Abs(forcePushPickup);
                    changeRight[1] = true;
                    changeLeft[1] = false;
                }
            }


        if(!pushOneTime)
        {
            
            rb2.AddForce(playerController.transform.right * forcePushPickup, ForceMode2D.Impulse);

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
