using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public bool ativo=false;
    public Animator anim;
    public AudioSource sourceButton;
    public AudioClip clipOpenButton;
    private bool playClipOneTime;

    void Start()
    {
       playClipOneTime = true;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag=="Player")
       {
           if(playClipOneTime){sourceButton.PlayOneShot(clipOpenButton); playClipOneTime = false;}
           ativo=true;
           anim.SetTrigger("Pre");
       } 
    }

    
}
