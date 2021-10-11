using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    // Estes GameObject serão preechidos pelos proprios prefab dos rockscontroller após instanciarem
    [SerializeField] public  List<float>         resistances = new List<float>(); 
    [SerializeField] public  List<GameObject>    rocksController = new List<GameObject>();
                     private Rigidbody2D         rb2;

    [Header("Inputs")]      
    [SerializeField] private bool                getKeyDownE;

    [Header("Atributtes PlayerControler")]
    [HideInInspector]public  float               axisHorizontal;
    [HideInInspector]private bool                dropRock;


    [Header("Atributtes Another Rocks")]
    [SerializeField] public  int                 sequenceIndexSubmit;   

    
    
    
    
     
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
        ControllerResistance();


    }

    void FixedUpdate()
    {


        
    }

    void Movimentation()
    {
        axisHorizontal  = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(axisHorizontal, rb2.velocity.y);
    }

    void ControllerResistance()
    {

        if(axisHorizontal != 0 && rocksController[sequenceIndexSubmit].
        GetComponent<NewRockController>().caterogyRockController == NewRockController.CaterogyRockController.controller
        )
        {
            resistances[sequenceIndexSubmit] -= 5 * Time.deltaTime;
        }

        if(resistances[sequenceIndexSubmit] < 0 && rocksController[sequenceIndexSubmit].
        GetComponent<NewRockController>().caterogyRockController == NewRockController.CaterogyRockController.controller)
        {
            rocksController[sequenceIndexSubmit].GetComponent<NewRockController>().caterogyRockController
            = NewRockController.CaterogyRockController.pickup;
            resistances[sequenceIndexSubmit] = 0;
        }

        for(int i = 0; i < resistances.Count; i++)
        {
            if(resistances[i] > 0 && rocksController[i].
            GetComponent<NewRockController>().caterogyRockController == NewRockController.CaterogyRockController.controller)
            {
                sequenceIndexSubmit = i;
                return ;
            }
        }
    }

    void ControllerOtherRocks()
    {

        // Apos clicar no input E, o controlador verifica qual dos gamobject esta com seu enum em "controller" 
        // busca ele, e o deixa em modo pickup

        if(getKeyDownE)
        {
            for(int i = 0; i < rocksController.Count; i++)
            {
                if(rocksController[i] != null && 
                rocksController[i].GetComponent<NewRockController>().caterogyRockController 
                == NewRockController.CaterogyRockController.controller && !dropRock)
                {
                    rocksController[i].GetComponent<NewRockController>().caterogyRockController 
                    = NewRockController.CaterogyRockController.pickup;
                    resistances[sequenceIndexSubmit] = 0;
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
        // Todos inputs contidos aqui para ser distribuidos para outros scripts atraves de booleanas
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            getKeyDownE = true;
        }else 
        {
            getKeyDownE = false;
        }
    }



}
