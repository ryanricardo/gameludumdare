using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
    [SerializeField] private bool cair;
    [SerializeField] Rigidbody2D RB2;
    void Start()
    {
        RB2.gravityScale=0;
    }

    
    void Update()
    {
        if(cair)
        {
            RB2.gravityScale=1;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            cair=true;
        }
    }
}
