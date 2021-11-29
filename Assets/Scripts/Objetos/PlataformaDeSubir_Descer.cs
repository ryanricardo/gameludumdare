using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDeSubir_Descer : MonoBehaviour
{
    [SerializeField]private float speed = 10.0f;
    [SerializeField]private Vector2 target;
    [SerializeField]private Vector2 position;
    [SerializeField]private Vector2 posicaoatual;
    public MecanismoDePeso mdp;
    private float step;
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

        if(mdp.ativo)
        {
            tempo-=Time.deltaTime;
        }
        else
        {
            tempo=tempoBase;
        }

        if(mdp.ativo && tempo<=0)
        {
           transform.position = Vector2.MoveTowards(transform.position, target, step);        
        }
        else
        {
             transform.position = Vector2.MoveTowards(transform.position, position, step);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            object1.transform.parent = object2.transform;
        }
    }

}
