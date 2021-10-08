using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [Header("Components")]
    // Estes GameObject serão preechidos pelos proprios prefab dos rockscontroller após instanciarem
    [SerializeField]public  GameObject[]    gameObjectsRocksController;
    [Header("Inputs")]      
    [SerializeField]private bool            getKeyDownE;
    [Header("Atributtes Another Rocks")]
    [SerializeField]public  int             countRocksController;
    [SerializeField]private bool            dropRock;
    
    
    
    
    void Start()
    {

    }

    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        ControllerOtherRocks();
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
