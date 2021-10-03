using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]private Rigidbody2D     rb2;
    [SerializeField]public  GameObject[]    rocksGameObject;
    [SerializeField]private GameObject[]    rocksPickup;
    [SerializeField]public  Transform       transformSpawnPosition;
    [Header("Inputs")]
    [SerializeField]private bool            getKeyDownSpace;
    [Header("Atributtes Movimentation")]
    [SerializeField]private float           axisHorizontal;
    [SerializeField]private float           speedVelocity;
    [SerializeField]private float           forceJump;
    [Header("Atributtes Resistance")]
    [SerializeField]private float           distanceSpawnRockPickup;
    [SerializeField]public  float[]         rocksResistances;    
    [SerializeField]private float[]         speedReduceRocksResistances;        
    [SerializeField]private float[]         speedAddRocksResistances;
    [SerializeField]public  bool []         rocksResistancesEnd;
    [SerializeField]public  bool []         applyOneTime;
    


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
        Inputs();

    }

    void Movimentation()
    {
        // Movimentação na horizontal 
        axisHorizontal = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal * speedVelocity, rb2.velocity.y);

        if(getKeyDownSpace && rb2.velocity.y <= 0)
        {
            rb2.AddForce(transform.up * forceJump, ForceMode2D.Impulse);
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
                Vector2 spawnRockPickup = new Vector2(transformSpawnPosition.transform.position.x + distanceSpawnRockPickup,
                transform.position.y);
                Instantiate(rocksPickup[0], spawnRockPickup, Quaternion.identity);
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
                Vector2 spawnRockPickup = new Vector2(transformSpawnPosition.transform.position.x + distanceSpawnRockPickup,
                transform.position.y);
                Instantiate(rocksPickup[1], spawnRockPickup, Quaternion.identity);
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
            if(rocksResistances[0] < 50 && rocksResistances[1] >= 50)
            {
                rocksResistances[0] += speedAddRocksResistances[0] * Time.deltaTime;
                
            }
            
            if(rocksResistances[1] < 50)
            {
                rocksResistances[1] += speedAddRocksResistances[1] * Time.deltaTime;
                
            }  
        }
    }


}

