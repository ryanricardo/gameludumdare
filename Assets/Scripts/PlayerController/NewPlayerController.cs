using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    private Transform[]                         transformChecksGround;
    [SerializeField]    private AudioSource                         source;
    [SerializeField]    private AudioClip                           clipJump;
    [HideInInspector]   private NewRockController.CategoryRock[]    categoriesRock;
    [HideInInspector]   public  Rigidbody2D                         rb2;
    [HideInInspector]   private GameObject[]                        rocks;

    [Header("Atributtes Movimentation")]
    [SerializeField]    private float                               speedMoviment;
    [SerializeField]    private float                               forceJump;
    [HideInInspector]   public  float                               axisHorizontal;
    [HideInInspector]   private bool[]                              checkGround;
    [HideInInspector]   public  bool                                isRight;
    [HideInInspector]   public  bool                                dropRock;

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
        dropRock = false;
        rb2 = GetComponent<Rigidbody2D>();
        categoriesRock = new NewRockController.CategoryRock[3];
        rocks = new GameObject[3];
        checkGround = new bool[3];
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

        checkGround[0] = Physics2D.Linecast(transform.position, transformChecksGround[0].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[1] = Physics2D.Linecast(transform.position, transformChecksGround[1].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[2] = Physics2D.Linecast(transform.position, transformChecksGround[2].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));


        if(checkGround[0] && getKeyDownSpace || 
        checkGround[1] && getKeyDownSpace ||
        checkGround[2] && getKeyDownSpace)
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
                    dropRock = true;
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
            balance -= speedSubmitBalance * Time.deltaTime;
        }else if(balance < maxBalance && axisHorizontal == 0)
        {
            balance += speedAddBalance * Time.deltaTime;
        }
        

    }

    void Inputs()
    {
        getKeyDownE = Input.GetKeyDown(KeyCode.E) ? getKeyDownE = true: getKeyDownE = false; 
        getKeyDownEsc = Input.GetKeyDown(KeyCode.Escape) ? getKeyDownEsc = true: getKeyDownEsc = false; 
        getKeyDownSpace = Input.GetKeyDown(KeyCode.Space) ? getKeyDownSpace = true: getKeyDownSpace = false; 
        getKeyDownR = Input.GetKeyDown(KeyCode.R) ? getKeyDownR = true: getKeyDownR = false; 

    }

}
