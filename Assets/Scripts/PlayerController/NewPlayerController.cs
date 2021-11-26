using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
       [Header("Components")]
    [SerializeField]    private Transform[]                         transformChecksGround;
    [SerializeField]    public  List<GameObject>                    rocks = new List<GameObject>();
    [SerializeField]    private AudioSource                         source;
    [SerializeField]    private AudioClip                           clipJump;
    [HideInInspector]   private NewRockController                   rockController;
    [HideInInspector]   public  Rigidbody2D                         rb2;
    [HideInInspector]   private Data                                data;

    [Header("Atributtes Movimentation")]
    [SerializeField]    private float                               speedMoviment;
    [SerializeField]    public  float                               forceJump;
    [SerializeField]    private float                               gravityAfterTouchVine;
    [SerializeField]    private float                               gravityBeforeTouchVine;
    [HideInInspector]   public  float                               axisHorizontal;
    [HideInInspector]   private bool[]                              checkGround;
    [HideInInspector]   public  bool                                isRight;
    [HideInInspector]   public  bool                                dropRock;

    
    [Header("Atributtes Balance")]
    [SerializeField]    public  float                               speedSubmitBalance;
    [SerializeField]    public  float                               speedAddBalance;
    [SerializeField]    public  int                                 maxBalance;
    [HideInInspector]   public  float                               balance;

    [Header("Inputs")]
    [HideInInspector]   public  bool                                getKeyDownE;
    [HideInInspector]   public  bool                                getKeyDownEsc;
    [HideInInspector]   public  bool                                getKeyDownSpace;
    [HideInInspector]   public  bool                                getKeyDownR;

    void Start()
    {
        rockController = FindObjectOfType<NewRockController>();
        rb2 = GetComponent<Rigidbody2D>();    
        balance = 100;
        checkGround = new bool[3];
        isRight = true;
        dropRock = true;
    }

    void Update()
    {


        Movimentation();
        ControllerRocks();
        ControllerBalance();
        Inputs();


    }

    void Movimentation()
    {
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

        for(int i = 1; i < rocks.Count; i++)
        {
            float scale = rocks[i].transform.localScale.x;
            scale *= -1;

            rocks[i].transform.localScale = 
              new Vector2(scale, rocks[i].transform.localScale.y);
        }
    }

    void ControllerRocks()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            rocks[rocks.Count - 1].GetComponent<NewRockController>().LeftGroup();
        }
    }

    void ControllerBalance()
    {

        if(axisHorizontal != 0 && balance > 0)
        {
            balance -= speedSubmitBalance * Time.deltaTime;
        }else if(balance < maxBalance && axisHorizontal == 0)
        {
            balance += speedAddBalance * Time.deltaTime;
        }
        

        if(balance <= 50)
        {
            for (int i = rocks.Count - 1; i > 1; i--)
            {
                if(rocks.Count != 1)
                {   
                    rocks[i].GetComponent<NewRockController>().LeftGroup();
                }
            }
        }

        switch(rocks.Count - 1)
        {
            
            case 0:
                maxBalance = 0;
                balance = 0;
            break;

            case 1:
                maxBalance = 50;
            break;

            case 2:
                maxBalance = 100;
            break;
        }

        
    }

    void Inputs()
    {
        getKeyDownE = Input.GetKeyDown(KeyCode.E) ? getKeyDownE = true: getKeyDownE = false; 
        getKeyDownEsc = Input.GetKeyDown(KeyCode.Escape) ? getKeyDownEsc = true: getKeyDownEsc = false; 
        getKeyDownSpace = Input.GetKeyDown(KeyCode.Space) ? getKeyDownSpace = true: getKeyDownSpace = false; 
        getKeyDownR = Input.GetKeyDown(KeyCode.R) ? getKeyDownR = true: getKeyDownR = false; 

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Vine"))
        {
            rb2.gravityScale = gravityAfterTouchVine;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Vine"))
        {
            rb2.gravityScale = gravityBeforeTouchVine;
        }
    }

}


