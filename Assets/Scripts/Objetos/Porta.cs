using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public Animator anim;
    public Botao botao;

    void Update()
    {
        if(botao.ativo)
        {
            anim.SetTrigger("Abrir");
        }
        else
        {
            anim.SetTrigger("Fechar");
        }
    }
}
