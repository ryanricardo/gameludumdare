using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTimer : MonoBehaviour
{
    public Animator anim;
    public BotaocomTimer bct;

    void Update()
    {
        if(bct.ativo)
        {
            anim.SetTrigger("Abrir");
        }
        else
        {
            anim.SetTrigger("Fechar");
        }
    }
}
