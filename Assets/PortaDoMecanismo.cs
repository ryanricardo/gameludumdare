using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaDoMecanismo : MonoBehaviour
{
    public Animator anim;
    public MecanismoDePeso mdp;

    void Update()
    {
        if(mdp.ativo)
        {
            anim.SetTrigger("Abrir");
        }
        else
        {
            anim.SetTrigger("Fechar");
        }
    }
}
