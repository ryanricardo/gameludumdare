using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] SpriteRenderer SR;
     void OnTriggerEnter2D(Collider2D col)
     {
         if(col.gameObject.tag=="Player")
         {
            SR.color= new Color(1,0.6f,0, 0.3f);
         }
     }
     void OnTriggerExit2D(Collider2D col)
     {
         if(col.gameObject.tag=="Player")
         {
            SR.color= new Color(1,0.6f,0, 1f);
         }
     }
}
