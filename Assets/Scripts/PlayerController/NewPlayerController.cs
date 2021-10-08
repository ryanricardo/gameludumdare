using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    // Estes GameObject serão preechidos pelos proprios prefab dos rockscontroller após instanciarem
    [SerializeField]public  GameObject[]    gameObjectsRocksController;
    private                 Rigidbody2D     rb2;
    [Header("Inputs")]      
    [SerializeField]private bool            getKeyDownE;
    [Header("Atributtes Another Rocks")]
    // A quantidade de pedras amontoadas é recebido pelo rockscontroller 
    [SerializeField]public  int             countRocksController;
    [SerializeField]private bool            dropRock;
    
    
    
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Inputs();
        Movimentation();
        // A função de dropar a pedra é baseada na troca de enum da propria pedra que a empurra do amontoado 
        // de pedras e tirar sua obrigação de permanecer junto ao eixo do grupo.
        ControllerOtherRocks();
    }

    void FixedUpdate()
    {

        
    }

    void Movimentation()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal, rb2.velocity.y);
    }

    void ControllerOtherRocks()
    {
        if(getKeyDownE)
        {
            for(int i = 0; i < gameObjectsRocksController.Length; i++)
            {
                if(gameObjectsRocksController[i] != null && 
                gameObjectsRocksController[i].GetComponent<NewRockController>().caterogyRockController 
                == NewRockController.CaterogyRockController.controller 
                && !dropRock)
                {
                    gameObjectsRocksController[i].GetComponent<NewRockController>().caterogyRockController 
                    = NewRockController.CaterogyRockController.pickup;

                    dropRock = true;
                }
            }
        }else 
        {
            dropRock = false;
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
    }


}
