using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    public bool chave=false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player" || col.gameObject.tag=="PedraController 0" || col.gameObject.tag=="PedraController 1")
        {
            chave=true;
            Destroy(gameObject);
        }
    }
}
