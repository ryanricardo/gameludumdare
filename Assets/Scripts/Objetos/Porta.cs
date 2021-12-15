using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public Animator anim;
    public Botao botao;
    public AudioSource sourcePorta;
    public AudioClip clipOpen;
    private bool playClipOneTime;

    void Update()
    {
        if(botao.ativo)
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
