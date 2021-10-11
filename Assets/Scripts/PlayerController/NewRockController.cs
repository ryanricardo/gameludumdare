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
    [SerializeField]private float                   forcePushPickup;
    [SerializeField]private float                   speedMoveNewPosition;
    [SerializeField]private bool                    pushOneTime;
    private                 bool                    moveNewPosition;
    [Header("Atributtes Controller")]
    [SerializeField]private bool                    someCountOneTime;
    [SerializeField]public  float                   resistance;


    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<NewPlayerController>();
        moveNewPosition = false;

        playerController.rocksController.Add(gameObject);
        playerController.resistances.Add(resistance);
        
        transform.position = new Vector2(playerController.transform.position.x, playerController.transform.position.x + 5);
    }

    void Update()
    {
        switch(caterogyRockController)
        {
            case CaterogyRockController.controller:
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
            speedMoveNewPosition);

            if(transform.position == newPos)
            {moveNewPosition = false;}
        }else 
        {
            transform.position = new Vector2(playerController.transform.position.x, transform.position.y);
        }

        
    }




    void PushThisPickup()
    {

        if(!pushOneTime)
        {
            rb2.AddForce(transform.right * forcePushPickup, ForceMode2D.Impulse);
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
                    pushOneTime = false;
                    moveNewPosition = true;
                    caterogyRockController = CaterogyRockController.controller;
                }

            break;
        }
    }


}
