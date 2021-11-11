using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDeSubir_Descer : MonoBehaviour
{
    [SerializeField]private float speed = 10.0f;
    [SerializeField]private float tempo = 2f;
    [SerializeField]private Vector2 target;
    [SerializeField]private Vector2 position;
    [SerializeField]private Vector2 posicaoatual;
    public MecanismoDePeso mdp;
    private float step;

    void Start()
    {
        position=transform.position;
    }

    void Update()
    {
        posicaoatual=transform.position;

        step = speed * Time.deltaTime;

        if(mdp.ativo)
        {
           transform.position = Vector2.MoveTowards(transform.position, target, step);        }
        else
        {
             transform.position = Vector2.MoveTowards(transform.position, position, step);
        }

    }

}
