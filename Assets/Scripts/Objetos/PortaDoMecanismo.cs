using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaDoMecanismo : MonoBehaviour
{
    public Animator anim;
    public MecanismoDePeso mdp;
    public AudioSource sourcePorta;
    public AudioClip clipOpen;
    private bool playClipOneTime;

    void Update()
    {
        if(mdp.ativo)
        {
            anim.SetTrigger("Abrir");
            if(playClipOneTime){sourcePorta.PlayOneShot(clipOpen); playClipOneTime = false;}
        }
        else
        {
            anim.SetTrigger("Fechar");
            playClipOneTime = true;
        }
    }
}
