using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueQuebra : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private BoxCollider2D BC2;
    [SerializeField] private BoxCollider2D BC2T;
    [SerializeField] private float tempoBase;
    [SerializeField] private float tempo;
    [SerializeField] private bool desgaste;
    
    void Start()
    {
        tempo=tempoBase;
    }

    
    void Update()
    {
        if(tempo>tempoBase)
        {
            tempo=tempoBase;
        }

        if(desgaste)
        {
            tempo-=Time.deltaTime;
        }

        if(!desgaste & tempo<tempoBase)
        {
            tempo+=Time.deltaTime;
        }

        if(tempo<0)
        {
            BC2.enabled=false;
            SR.enabled=false;
            BC2T.enabled=false;
        }
        else if(tempo==tempoBase)
        {
            BC2.enabled=true;
            SR.enabled=true;
            BC2T.enabled=true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player" || col.gameObject.tag=="pedra")
      {
         desgaste=true;
      }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player" || col.gameObject.tag=="pedra")
        {
            desgaste=false;
        }
    }

}
