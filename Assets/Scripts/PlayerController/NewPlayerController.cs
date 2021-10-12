using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector]   private Rigidbody2D                         rb2;
    [SerializeField]    private GameObject[]                        rocks;
    [SerializeField]    private NewRockController.CategoryRock[]    categoriesRock;
    [Header("Atributtes Movimentation")]
    [SerializeField]    private float                               axisHorizontal;
    [Header("Atributtes Balance")]
    [SerializeField]    public  float                               balance;
    [SerializeField]    public  float                               speedSubmitBalance;
    [SerializeField]    public  float                               speedAddBalance;
    [SerializeField]    private int                                 maxBalance;
    [SerializeField]    private int                                 countRocks;



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
                if(categoriesRock[i] == NewRockController.CategoryRock.controller)
                {
                    rocks[i].GetComponent<NewRockController>().categoryRock 
                    = NewRockController.CategoryRock.pickup;
                    return;
                }
            }
        }
    }

    void ControllerBalance()
    {

        /* Nesta linha é adicionado dentro de um array[3] as variaveis de enum das duas pedras controller*/

        for(int i = 1; i < rocks.Length; i++)
        {
            categoriesRock[i] = rocks[i].gameObject.GetComponent<NewRockController>().categoryRock;
        }


        /* Estas linhas controlam o numero de pedras pertences ao grupo procurando quanto gameObject 
        Estão com seu enum em controller. */

        if(categoriesRock[1] == NewRockController.CategoryRock.controller
        && categoriesRock[2] == NewRockController.CategoryRock.controller)
        {
            countRocks = 2;
        }else 
        {
            if(categoriesRock[1] != NewRockController.CategoryRock.controller
            && categoriesRock[2] == NewRockController.CategoryRock.controller)
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
            balance -= speedSubmitBalance * Time.fixedDeltaTime;
        }else if(balance < maxBalance)
        {
            balance += speedAddBalance * Time.fixedDeltaTime;
        }
        

    }


}
