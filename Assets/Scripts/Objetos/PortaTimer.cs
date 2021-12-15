using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTimer : MonoBehaviour
{
    public Animator anim;
    public BotaocomTimer bct;
    public AudioSource sourcePorta;
    public AudioClip clipOpen;
    private bool playClipOneTime;

    void Update()
    {
        if(bct.ativo)
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
