using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickup : MonoBehaviour
{
    enum NameObject
    {
        Rock0,
        Rock1,
    }

    [Header("Components")]
    [SerializeField]private PlayerController    playerController;
    [SerializeField]private Transform           transformSpawnPos;
    [SerializeField]private GameObject[]        gameObjectsRocks;
    [SerializeField]private NameObject          nameObject;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        transformSpawnPos = FindObjectOfType<PlayerController>().transformSpawnPosition;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerController.rocksResistancesEnd[0] && nameObject == NameObject.Rock0)
            {
                Instantiate(gameObjectsRocks[0], 
                transformSpawnPos.transform.position, Quaternion.identity);
                Destroy(gameObject, 0);
            }else if(playerController.rocksResistancesEnd[1] && nameObject == NameObject.Rock1)
            {
                Instantiate(gameObjectsRocks[1], 
                transformSpawnPos.transform.position, Quaternion.identity);
                Destroy(gameObject, 0);
            }

        }
    }
}
