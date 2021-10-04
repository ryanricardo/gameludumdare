using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]private Rigidbody2D     rb2;
    [SerializeField]public  GameObject[]    rocksGameObject;
    [SerializeField]private GameObject[]    rocksPickup;
    [SerializeField]private RaycastHit2D    physics2DCheckGround;
    [SerializeField]public  Transform       transformSpawnPosition;
    [SerializeField]public  Transform       transformSpawnRockPickup;
    [SerializeField]private Transform       transformCheckGround;
    [Header("Inputs")]
    [SerializeField]private bool            getKeyDownSpace;
    [SerializeField]private bool            getKeyDownE;
    [Header("Atributtes Movimentation")]
    [SerializeField]private float           axisHorizontal;
    [SerializeField]private float           speedVelocityCurrent;
    [SerializeField]private float           speedVelocity2Rocks;
    [SerializeField]private float           speedVelocity3Rocks;
    [SerializeField]private float           forceJumpCurrent;
    [SerializeField]private float           forceJump3Rocks;
    [SerializeField]private float           forceJump2Rocks;
    [SerializeField]private bool            isRight;
    [SerializeField]private bool            checkGround;
    [Header("Atributtes Resistance")]
    [SerializeField]private float           distanceSpawnRockPickup;
    [SerializeField]public  float[]         rocksResistances;    
    [SerializeField]private float[]         speedReduceRocksResistances;        
    [SerializeField]private float[]         speedAddRocksResistances;
    [SerializeField]public  bool []         rocksResistancesEnd;
    [SerializeField]public  bool []         applyOneTime;
    [Header("Atributtes Drop Rock")]
    [SerializeField]private float           distanceDropRock;
    


    void Start()
    {
        rocksGameObject[0] = GameObject.FindWithTag("PedraController 0");
        rocksGameObject[1] = GameObject.FindWithTag("PedraController 1");
    }

    void Update()
    {

        Movimentation();
        UpdateCheckResistance();
        UpdateResistance();
        DropRock();
        Inputs();

    }

    void Movimentation()
    {
        // Movimentação na horizontal 
        rocksGameObject[0] = GameObject.FindWithTag("PedraController 0");
        rocksGameObject[1] = GameObject.FindWithTag("PedraController 1");

        if(rocksGameObject[0] == null)
        {
            speedVelocityCurrent = speedVelocity2Rocks;
        }else 
        {
            speedVelocityCurrent = speedVelocity3Rocks;
        }

        axisHorizontal = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal * speedVelocityCurrent, rb2.velocity.y);

        checkGround = Physics2D.Linecast(
        transform.position, transformCheckGround.transform.position, 1 << LayerMask.NameToLayer("Chao"));
        

        if(getKeyDownSpace && checkGround)
        {

            if(rocksGameObject[0] == null)
            {
                forceJumpCurrent = forceJump2Rocks;
            }
            else 
            {
                forceJumpCurrent = forceJump3Rocks;
            }

            rb2.AddForce(transform.up * forceJumpCurrent, ForceMode2D.Impulse);
        }

        if(axisHorizontal < 0 && isRight)
        {
            Flip();
            isRight = false;
            distanceDropRock = distanceDropRock * -1;
        }else if(axisHorizontal > 0 && !isRight)
        {
            Flip();
            isRight = true;
            distanceDropRock = distanceDropRock * -1;
        }
    }



    void Flip()
    {
        Vector2 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    void DropRock()
    {
        if(getKeyDownE)
        {      
            rocksGameObject[0] = GameObject.FindWithTag("PedraController 0");
            rocksGameObject[1] = GameObject.FindWithTag("PedraController 1");

            if(rocksGameObject[0] != null && !rocksResistancesEnd[1])
            {
                Vector2 spawnDropWeapon = new Vector2(transform.position.x + distanceDropRock, 
                transform.position.y);
                Instantiate(rocksPickup[0], spawnDropWeapon, Quaternion.identity);  
                Destroy(rocksGameObject[0], 0);
                rocksResistances[0] = 0;
                rocksResistancesEnd[0] = true;
                Debug.Log("Drop");
            }else if(rocksGameObject[1] != null && !rocksResistancesEnd[0])
            {
                Vector2 spawnDropWeapon = new Vector2(transform.position.x + distanceDropRock, 
                transform.position.y);
                Instantiate(rocksPickup[1], spawnDropWeapon, Quaternion.identity);  
                Destroy(rocksGameObject[1], 0);
                rocksResistances[1] = 0;
                rocksResistancesEnd[1] = true;
                Debug.Log("Drop");
            }

        }
    }

    void Inputs()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            getKeyDownSpace = true;
        }else 
        {
            getKeyDownSpace = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            getKeyDownE = true;
        }else 
        {
            getKeyDownE = false;
        }
    }


    void UpdateCheckResistance()
    {
        //Controlador de resistencias que acabaram e 
        //spawnador da rocksPickup para recuperar a resistencia novamente 

        if(rocksResistances[0] <= 0 && !rocksResistancesEnd[0])
        {
            if(applyOneTime[0])
            {
                rocksGameObject[0] = GameObject.FindWithTag("PedraController 0");
                rocksResistancesEnd[0] = true;
                Instantiate(rocksPickup[0], 
                transformSpawnRockPickup.transform.position, Quaternion.identity);
                Destroy(rocksGameObject[0], 0);
                applyOneTime[0] = false;
            }
            
        }

        if(rocksResistances[1] <= 0 && !rocksResistancesEnd[1])
        {
            if(applyOneTime[1])
            {
                rocksGameObject[1] = GameObject.FindWithTag("PedraController 1");
                rocksResistancesEnd[1] = true;
                Instantiate(rocksPickup[1], 
                transformSpawnRockPickup.transform.position, Quaternion.identity);
                Destroy(rocksGameObject[1], 0);
                applyOneTime[1] = false;
            }
            
        }
    }


    void UpdateResistance()
    {

        //Controlador da resistencia da pedras

        if(axisHorizontal != 0)
        {
            if(rocksResistances[0] > 0)
            {
                rocksResistances[0] -= speedReduceRocksResistances[0] * Time.deltaTime;
            } else 
            {
                if(rocksResistances[1] > 0)
                {
                    rocksResistances[1] -= speedReduceRocksResistances[1] * Time.deltaTime;
                }
            }
        }else 
        {
            if(rocksResistances[0] < 50 && !rocksResistancesEnd[0])
            {
                rocksResistances[0] += speedAddRocksResistances[0] * Time.deltaTime;
                
            }
            
            if(rocksResistances[1] < 50 && !rocksResistancesEnd[1])
            {
                rocksResistances[1] += speedAddRocksResistances[1] * Time.deltaTime;
                
            }  
        }
    }

}

