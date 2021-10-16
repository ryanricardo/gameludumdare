using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFall : MonoBehaviour
{
    
    private void FixedUpdate(){
        
        // Fall and off screen
        if (transform.position.y < -8){
            Vector3 newPos = Vector3.zero;
            newPos.x = transform.position.x - 1.5f;
            newPos.y = 2; 
            transform.position = newPos;
        }

    }
}
