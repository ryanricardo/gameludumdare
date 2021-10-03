using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaocomTimer : MonoBehaviour
{
    public bool ativo=false;
    public Animator anim;
    public Chave chave;
    public bool funciona;

    void Update()
    {
        if(chave.chave)
        {
            funciona=true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player" || col.gameObject.tag=="Pickup" && funciona)
       {
           ativo=true;
           anim.SetTrigger("Pre");
       } 
    }

    void OnTriggerExit2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player")
       {
           StartCoroutine("Abrir");
       } 
    }

     private IEnumerator Abrir()
     {
         ativo=true;
         yield return new WaitForSeconds(5f);
         ativo=false;
         anim.SetTrigger("Normal");
     }
}
