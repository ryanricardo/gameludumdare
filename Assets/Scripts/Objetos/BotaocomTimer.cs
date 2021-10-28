using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaocomTimer : MonoBehaviour
{
    public bool ativo=false;
    public Animator anim;
    public Chave chave;
    public bool funciona=false;
    public float tempo;

    void Update()
    {
        if(chave.chave)
        {
            funciona=true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player" && funciona|| col.gameObject.tag=="RockController 1" && funciona 
        || col.gameObject.tag == "RockController 2"  && funciona)
       {
           ativo=true;
           anim.SetTrigger("Pre");
       } 
    }

    void OnTriggerExit2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player" || col.gameObject.tag=="RockController 1" 
        || col.gameObject.tag == "RockController 2")
       {
           StartCoroutine("Abrir");
       } 
    }

     private IEnumerator Abrir()
     {
         yield return new WaitForSeconds(tempo);
         ativo=false;
         anim.SetTrigger("Normal");
     }
}
