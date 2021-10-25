using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    private Transform                           transformCheckGround;
    [SerializeField]    private AudioSource                         source;
    [SerializeField]    private AudioClip                           clipJump;
    [HideInInspector]   private NewRockController.CategoryRock[]    categoriesRock;
    [HideInInspector]   private Rigidbody2D                         rb2;
    [HideInInspector]   private GameObject[]                        rocks;

    [Header("Atributtes Movimentation")]
    
    [SerializeField]    private float                               speedMoviment;
    [SerializeField]    private float                               forceJump;
    [HideInInspector]   public  float                               axisHorizontal;
    [HideInInspector]   private bool                                checkGround;
    [HideInInspector]   public  bool                                isRight;

    [Header("Atributtes Balance")]

    [SerializeField]    public  float                               speedSubmitBalance;
    [SerializeField]    public  float                               speedAddBalance;
    [HideInInspector]   private int                                 maxBalance;
    [HideInInspector]   private int                                 countRocks;
    [HideInInspector]   public  float                               balance;


    [Header("Inputs")]
    [HideInInspector]   public  bool                                getKeyDownE;
    [HideInInspector]   public  bool                                getKeyDownEsc;
    [HideInInspector]   public  bool                                getKeyDownSpace;
    [HideInInspector]   public  bool                                getKeyDownR;



    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        categoriesRock = new NewRockController.CategoryRock[3];
        rocks = new GameObject[3];
        for(int i = 1; i < rocks.Length; i++)
        {
            rocks[i] = GameObject.FindGameObjectWithTag("RockController " + i);
        }
        balance = 100;
        isRight = true;
    }

    void Update()
    {
        Movimentation();
        ControllerBalance();
        ControllerDropRock();
        Inputs();
    }

    void Movimentation()
    {
        // Movimentação basica envolvendo apenas o axis horizontal e rigidbody2d

        axisHorizontal = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal * speedMoviment, rb2.velocity.y);
        checkGround = Physics2D.Linecast(transform.position, transformCheckGround.transform.position, 
        1 << LayerMask.NameToLayer("Chao"));


        if(getKeyDownSpace && checkGround)
        {
            rb2.AddForce(transform.up * forceJump, ForceMode2D.Impulse);
            source.PlayOneShot(clipJump);
        }

        if(axisHorizontal < 0 && isRight)
        {
            Flip();
            isRight = false;
        }else if(axisHorizontal > 0 && !isRight)
        {
            Flip();
            isRight = true;
        }


    }

    void Flip()
    {
        float scl = transform.localScale.x;
        scl *= -1;
        transform.localScale = new Vector2(scl, transform.localScale.y);

        for(int i = 1; i < rocks.Length; i++)
        {
            float scale = rocks[i].transform.localScale.x;
            scale *= -1;

            rocks[i].transform.localScale = 
              new Vector2(scale, rocks[i].transform.localScale.y);
        }
    }

    void ControllerDropRock()
    {

        /* Aqui, após clicar clicar "E", é identificado pelo for a primeira pedra que esta em modo controller. 
        Após isso, o deixa em modo pickupe return. */

        // Vale ressaltar que nos arrays criado o numero 0 NÃO tem valor algum, deve desconsiderar.
        
        if(getKeyDownE)
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

    void Inputs()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            getKeyDownE = true;
        }else 
        {
            getKeyDownE = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            getKeyDownEsc = true;
        }else 
        {
            getKeyDownEsc = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            getKeyDownSpace = true;
        }else 
        {
            getKeyDownSpace = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            getKeyDownR = true;
        }else 
        {
            getKeyDownR = false;
        }
    }

}
