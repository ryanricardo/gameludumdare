using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRockController : MonoBehaviour
{
    public enum CaterogyRockController
    {
        controller,
        pickup,
    }

    enum IndexRockController
    {
        rock0,
        rock1,
    }

    [Header("Components")]
    [SerializeField]private IndexRockController     indexRockController;
    [SerializeField]public  CaterogyRockController  caterogyRockController;
    private                 NewPlayerController     playerController;
    private                 Rigidbody2D             rb2;
    [Header("Atributtes Pickup")]
    [SerializeField]private bool                    pushOneTime;
    [SerializeField]private bool                    moveNewPosition;
    [SerializeField]private float                   forcePushPickup;
    [SerializeField]private float                   speedMoveNewPosition;
    [Header("Atributtes Controller")]
    [SerializeField]private bool                    someCountOneTime;


    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();

        
        switch(indexRockController)
        {
            case IndexRockController.rock0:
            playerController.gameObjectsRocksController[0] = GameObject.FindGameObjectWithTag("PedraController 0");
            break;

            case IndexRockController.rock1:
            playerController.gameObjectsRocksController[1] = GameObject.FindGameObjectWithTag("PedraController 1");
            break;
        }


        transform.position = new Vector2(playerController.transform.position.x, playerController.transform.position.x + 5);
    }

    void Update()
    {

        switch(caterogyRockController)
        {
            case CaterogyRockController.controller:
            someCountRocks();
            Movimentation();
            break;

            case CaterogyRockController.pickup:
            PushThisPickup();
            break;
        }
        
    }

    void Movimentation()
    {
        if(moveNewPosition)
        {
            Vector3 newPos = new Vector3(playerController.transform.position.x,
            playerController.transform.position.y + 2, transform.position.z);
            transform.position = Vector2.MoveTowards(transform.position, newPos, 
            speedMoveNewPosition * Time.fixedDeltaTime);

            if(transform.position == newPos)
            {moveNewPosition = false;}
        }else 
        {
            transform.position = new Vector2(playerController.transform.position.x, transform.position.y);
        }

        
    }

    void someCountRocks()
    {
        if(!someCountOneTime)
        {
            playerController.countRocksController += 1;
            pushOneTime = false;

            someCountOneTime = true;
        }
        
    }

    void PushThisPickup()
    {

        if(!pushOneTime)
        {
            rb2.AddForce(transform.right * forcePushPickup, ForceMode2D.Impulse);
            playerController.countRocksController -= 1;
            someCountOneTime = false;

            pushOneTime = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch(caterogyRockController)
        {
            case CaterogyRockController.controller:

            break;

            case CaterogyRockController.pickup:

                if(other.gameObject.CompareTag("Player"))
                {
                    //transform.position = new Vector2(transform.position.x, 
                    //playerController.transform.position.y + 10);
                    moveNewPosition = true;
                    caterogyRockController = CaterogyRockController.controller;
                }

            break;
        }
    }


}
