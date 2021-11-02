using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismoDePeso : MonoBehaviour
{
    [SerializeField] public float PesoNecessario;
    [SerializeField] public float PesoAtual;
    [SerializeField] public bool ativo=false;

    void Update()
    {
        if(PesoAtual>=PesoNecessario)
        {
            ativo=true;
        }
        else
        {
            ativo=false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player"|| col.gameObject.tag=="pedra")
        {
            PesoAtual++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player" || col.gameObject.tag=="pedra")
        {
            PesoAtual--;
        }
    }

}
