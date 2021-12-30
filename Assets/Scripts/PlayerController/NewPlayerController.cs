using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    private Transform[]                         transformChecksGround;
    [SerializeField]    private Transform                           transformCheckFall;
    [SerializeField]    public  List<GameObject>                    rocks = new List<GameObject>();
    [SerializeField]    public  List<GameObject>                    rocksOut = new List<GameObject>();
    [SerializeField]    public  AudioSource                         source;
    [SerializeField]    private AudioClip                           clipJump;
    [SerializeField]    private AudioClip                           clipFall;
    [SerializeField]    private AudioClip                           clipRun;
    [HideInInspector]   private Lava                                lava;
    [HideInInspector]   private Joystick                            joystick;
    [HideInInspector]   private NewRockController                   rockController;
    [HideInInspector]   public  Rigidbody2D                         rb2;
    [HideInInspector]   private Data                                data;

    [Header("Atributtes Movimentation")]
    [SerializeField]    private float                               speedMoviment;
    [SerializeField]    public  float                               forceJump;
    [HideInInspector]   public  float                               axisHorizontal;
    [HideInInspector]   private bool[]                              checkGround;
    [HideInInspector]   private bool                                checkFall;
    [HideInInspector]   public  bool                                isRight;
    [HideInInspector]   public  bool                                dropRock;
    [HideInInspector]   private bool                                slowMov;
    [HideInInspector]   private bool                                jump;
    
    [Header("Atributtes Body")]
    [SerializeField]    public  float                               temp;
    [HideInInspector]   private bool                                hurtMagma;
    

    [Header("Atributtes Balance")]
    [SerializeField]    public  float                               speedSubmitBalance;
    [SerializeField]    public  float                               speedAddBalance;
    [SerializeField]    public  int                                 maxBalance;
    [SerializeField]    public  float                               balance;

    void Start()
    {
        source.volume = PlayerPrefs.GetFloat("VolumeEffectsGame");
        jump = false;
        temp = 0;
        lava = FindObjectOfType<Lava>();
        rockController = FindObjectOfType<NewRockController>();
        joystick = FindObjectOfType<Joystick>();
        rb2 = GetComponent<Rigidbody2D>();    
        balance = 100;
        checkGround = new bool[3];
        isRight = true;
        dropRock = true;
    }

    void Update()
    {


        Movimentation();
        ControllerBalance();
        ControllerTemp();


    }

    public void Movimentation()
    {

        rb2.velocity = new Vector2 (joystick.Horizontal * speedMoviment, rb2.velocity.y);

        if(rb2.velocity.x < 0 && isRight)
        {
            Flip();
            isRight = false;
        }else if(rb2.velocity.x > 0 && !isRight)
        {
            Flip();
            isRight = true;
        }


        if(rb2.velocity.x != 0 && balance > 1)
        {
            checkGround[1] = Physics2D.Linecast(transform.position, transformChecksGround[1].transform.position, 
            1 << LayerMask.NameToLayer("Chao"));

            if(checkGround[1]&&
            !source.isPlaying)
            {
                source.clip = clipRun;
                source.Play();
            }
        }else if(rb2.velocity.x == 0 && 
        source.clip == clipRun)
        {
            source.clip = null;
            source.Stop();
        }

        if(jump)
        {
            checkFall = Physics2D.Linecast(transform.position, transformCheckFall.transform.position, 1 << LayerMask.NameToLayer("Chao"));
            if(checkFall)
            {
                source.PlayOneShot(clipFall);
                jump = false;
            }
        }
    }

    void Flip()
    {
        float scl = transform.localScale.x;
        scl *= -1;
        transform.localScale = new Vector2(scl, transform.localScale.y);
    }

    public void Jump()
    {


        checkGround[0] = Physics2D.Linecast(transform.position, transformChecksGround[0].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[1] = Physics2D.Linecast(transform.position, transformChecksGround[1].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[2] = Physics2D.Linecast(transform.position, transformChecksGround[2].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));

        if(checkGround[0] || 
        checkGround[1] ||
        checkGround[2])
        {
            StartCoroutine(TrueJump());
            rb2.AddForce(transform.up * forceJump, ForceMode2D.Impulse);
            source.PlayOneShot(clipJump);
        }
    }

    public void DropRock()
    {
        
        if(isRight && rocks.Count >= 3)
        {
            rocks[rocks.Count - 1].GetComponent<NewRockController>().LeftGroup(Vector2.right);
        }else if(!isRight && rocks.Count >= 3)
        {
            rocks[rocks.Count - 1].GetComponent<NewRockController>().LeftGroup(Vector2.left);                    
        }
        
    }

    void ControllerBalance()
    {

        if(rb2.velocity.x != 0 && balance > 0)
        {
            balance -= speedSubmitBalance * Time.deltaTime;
        }else if(balance < maxBalance && rb2.velocity.x == 0)
        {
            balance += speedAddBalance * Time.deltaTime;
        }
        

        if(balance <= 50)
        {
            LeftGroupRocks();
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

    public void LeftGroupRocks()
    {
        for (int i = rocks.Count - 1; i > 1; i--)
            {
                if(rocks.Count != 1)
                {   
                    if(isRight)
                    {
                        rocks[i].GetComponent<NewRockController>().LeftGroup(Vector2.left);
                    }else 
                    {
                        rocks[i].GetComponent<NewRockController>().LeftGroup(Vector2.right);                    
                    }
                }
            }
    }

    void ControllerTemp()
    {

        if(hurtMagma && temp < 100)
        {
            temp += lava.damage * Time.deltaTime;
            if(temp >= 100)
            {
                LeftGroupRocks();
            }
        }else if(!hurtMagma && temp > 0)
        {
            temp -= lava.damage * Time.deltaTime;
        }
    }
    
    public void PushCollisionRocks()
    {

        // Este metodo é ativado pelo script "NewRockController"


        checkGround[0] = Physics2D.Linecast(transform.position, transformChecksGround[0].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[1] = Physics2D.Linecast(transform.position, transformChecksGround[1].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));
        checkGround[2] = Physics2D.Linecast(transform.position, transformChecksGround[2].transform.position, 
        1 << LayerMask.NameToLayer("Chao"));

        if(!checkGround[0] &&
        !checkGround[1] &&
        !checkGround[2])
        {
            rb2.AddForce(Vector2.down * 1000);
            for(int i = 1; i < rocks.Count; i++)
            {
                rocks[i].GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000);
            }
        }else 
        {
            if(isRight && checkGround[0] ||
            isRight && checkGround[1] ||
            isRight && checkGround[2])
            {
                rb2.AddForce(Vector2.left * 1000);
            }else if(!isRight && checkGround[0] ||
            !isRight && checkGround[1] ||
            !isRight && checkGround[2])
            {
                rb2.AddForce(Vector2.right * 1000);
            }
        }
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Vine"))
        {
            
            for(int i = 0; i < rocks.Count ; i++)
            {
                rocks[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                rocks[i].GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            }

        }

        if(other.gameObject.CompareTag("Lava"))
        {
            hurtMagma = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Vine"))
        {
            for(int i = 0; i < rocks.Count ; i++)
            {
                rocks[i].GetComponent<Rigidbody2D>().gravityScale = 1f;
            }

            for(int i = 0; i < rocksOut.Count; i++)
            {
                rocksOut[i].GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
        }

        if(other.gameObject.CompareTag("Lava"))
        {
            hurtMagma = false;
        }
    }

    IEnumerator TrueJump()
    {
        yield return new WaitForSeconds(1);
        jump = true;
    }

}
