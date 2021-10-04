using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersPosition : MonoBehaviour
{
    enum NameObject
    {
        rock0,
        rock1,
    }

    [Header("Components")]
    [SerializeField]private PlayerController    playerController;
    [Header("Atributtes Rock")]
    [SerializeField]public  bool                StartOneTime;
    [SerializeField]private NameObject          nameObject;
    

    void Start()
    {

        if(StartOneTime)
        {

            StartOneTime = false;
        }else 
        {   
            playerController = FindObjectOfType<PlayerController>();
            


            if(playerController.rocksResistancesEnd[1] && nameObject == NameObject.rock1)
            {   
                playerController.applyOneTime[1] = true;
                playerController.rocksResistancesEnd[1] = false;
                playerController.rocksResistances[1]    = 5;
            }else if(playerController.rocksResistancesEnd[0] && nameObject == NameObject.rock0)
            {   
                playerController.applyOneTime[0] = true;
                playerController.rocksResistancesEnd[0] = false;
                playerController.rocksResistances[0]    = 5;
            }


        }
        

    }

    void Update()
    {
       FixedPositionPlayer();
    }

    void FixedPositionPlayer()
    {
        // Segurar o eixo X no player e no Y ficar solto
        playerController = FindObjectOfType<PlayerController>();
        transform.position = new Vector2(playerController.transform.position.x, transform.position.y);
    }



}
