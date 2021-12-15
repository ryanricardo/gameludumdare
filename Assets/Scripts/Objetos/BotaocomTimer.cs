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
    public AudioSource sourceButton;
    public AudioClip clipOpenButton;
    private bool playClipOneTime;

    void Start()
    {
        playClipOneTime = true;
    }
    void Update()
    {
        
        if(chave.chave)
        {
            funciona=true;
        }
    }

    public void EnterRockButton(Transform t, GameObject g)
    {
        t.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
        g.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        sourceButton.PlayOneShot(clipOpenButton);
        Debug.Log("d");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player" && funciona|| col.gameObject.tag=="RockController 1" && funciona 
        || col.gameObject.tag == "RockController 2"  && funciona)
       {
           sourceButton.PlayOneShot(clipOpenButton);
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
        sourceButton.PlayOneShot(clipOpenButton);
         ativo=false;
         anim.SetTrigger("Normal");
     }
}
