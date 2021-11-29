using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraDePeso : MonoBehaviour
{
    [SerializeField]private float speed = 10.0f;
    [SerializeField]private Vector2 target;
    [SerializeField]private Vector2 position;
    [SerializeField]private Vector2 posicaoatual;
    private float step;
    [SerializeField] public float PesoNecessario;
    [SerializeField] public float PesoAtual;
    [SerializeField] public bool ativo=false;
    [SerializeField]private float tempoBase;
    [SerializeField]private float tempo;
    [SerializeField]private GameObject object1;
    [SerializeField]private GameObject object2;

    void Start()
    {
        position=transform.position;
        tempo=tempoBase;
    }

    void Update()
    {
        posicaoatual=transform.position;

        step = speed * Time.deltaTime;

        if(ativo)
        {
            tempo-=Time.deltaTime;
        }
        else
        {
            tempo=tempoBase;
        }

        if(PesoAtual>=PesoNecessario)
        {
            ativo=true;
        }
        else
        {
            ativo=false;
        }

        if(ativo && tempo<=0)
        {
           transform.position = Vector2.MoveTowards(transform.position, target, step);        }
        else
        {
             transform.position = Vector2.MoveTowards(transform.position, position, step);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="RockController 1" || col.gameObject.tag=="RockController 2" || col.gameObject.tag== "Player"|| col.gameObject.tag=="pedra")
        {
            PesoAtual++;
            object1.transform.parent = object2.transform;
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
