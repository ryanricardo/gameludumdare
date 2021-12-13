using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mato_Seco : MonoBehaviour
{
    [SerializeField]float tempNecessaria;
    NewPlayerController newPlayerController;
    void Start()
    {
        newPlayerController = FindObjectOfType<NewPlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag=="Player")
       {
           if(newPlayerController.temp>=tempNecessaria)
           {
             Destroy(gameObject);
           }
       }
    }
}
