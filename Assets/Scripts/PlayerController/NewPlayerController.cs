using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector]   private Rigidbody2D     rb2;
    [SerializeField]    private GameObject[]    rocks;
    [Header("Atributtes Movimentation")]
    [SerializeField]    private float           axisHorizontal;
    [Header("Atributtes Balance")]
    [SerializeField]    public  float           balance;
    [SerializeField]    private int             maxBalance;
    [SerializeField]    private int             countRocks;


    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        balance = 100;
    }

    void Update()
    {
        Movimentation();
        ControllerBalance();
        ControllerDropRock();
    }

    void Movimentation()
    {
        // Movimentação basica envolvendo apenas o axis horizontal e rigidbody2d

        axisHorizontal = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal, rb2.velocity.y);
    }

    void ControllerDropRock()
    {

        /* Aqui, após clicar clicar "E", é identificado pelo for a primeira pedra que esta em modo controller. 
        Após isso, o deixa em modo pickupe return. */

        if(Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 1; i < rocks.Length; i++)
            {
                if(rocks[i].GetComponent<NewRockController>().categoryRock 
                == NewRockController.CategoryRock.controller)
                {
                    rocks[i].GetComponent<NewRockController>().categoryRock = NewRockController.CategoryRock.pickup;
                    return;
                }
            }
        }
    }

    void ControllerBalance()
    {
        /* Estas linhas controlam o numero de pedras pertences ao grupo procurando quanto gameObject 
        Estão com seu enum em controller. */

        if(rocks[1].GetComponent<NewRockController>().categoryRock == NewRockController.CategoryRock.controller
        && rocks[2].GetComponent<NewRockController>().categoryRock == NewRockController.CategoryRock.controller)
        {
            countRocks = 2;
        }else 
        {
            if(rocks[1].GetComponent<NewRockController>().categoryRock != NewRockController.CategoryRock.controller
            && rocks[2].GetComponent<NewRockController>().categoryRock == NewRockController.CategoryRock.controller)
            {
                countRocks = 1;
            }else 
            {
                countRocks = 0;
            }
            
        } 

        
        // Aqui, é utilizado o numero de pedras pertencentes ao grupo para controlar o maximo de equilibrio.

        switch(countRocks)
        {
            case 2:
            maxBalance = 100;
            break;

            case 1:
            maxBalance = 50;
            break;

            case 0:
            maxBalance = 0;
            balance = 0;
            break;
        }

        // Aqui, é somado ou subtraido o equilibrio ao jogador andar mantendo restrito ao maximo de equilibrio;
        
         
        if(axisHorizontal != 0 && balance > 0)
        {
            balance -= 5 * Time.fixedDeltaTime;
        }else if(balance < maxBalance)
        {
            balance += 5 * Time.fixedDeltaTime;
        }
        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("RockController") 
        && other.gameObject.GetComponent<NewRockController>().categoryRock 
        == NewRockController.CategoryRock.pickup)
        {
            
            balance += 50;
        }
    }

}
