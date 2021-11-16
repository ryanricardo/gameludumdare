using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueQuebra : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private BoxCollider2D BC2;
    [SerializeField] private bool destruido=false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(destruido)
        {
            SR.enabled=false;
            BC2.enabled=false;
        }
        else
        {
            SR.enabled=true;
            BC2.enabled=true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player"|| col.gameObject.tag=="pedra")
       {
           
       }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player"|| col.gameObject.tag=="pedra")
       {

       }
    }
}
