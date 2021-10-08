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
    [SerializeField]private float                   forcePushPickup;


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
            Movimentation();
            break;

            case CaterogyRockController.pickup:
            PushThisPickup();
            break;
        }
        
    }

    void Movimentation()
    {
        transform.position = new Vector2(playerController.transform.position.x, transform.position.y);
    }

    void PushThisPickup()
    {

        if(!pushOneTime)
        {
            rb2.AddForce(transform.right * forcePushPickup, ForceMode2D.Impulse);
            pushOneTime = true;
        }

    }


}
