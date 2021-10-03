using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public bool ativo=false;
    public Animator anim;

    void Start()
    {
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player")
       {
           ativo=true;
           anim.SetTrigger("Pre");
       } 
    }

    
}
