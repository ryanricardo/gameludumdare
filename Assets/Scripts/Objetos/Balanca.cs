using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balanca : MonoBehaviour
{
    Quaternion startRot;
    private void Start()
    {
        transform.rotation = startRot;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.SetPositionAndRotation(transform.position, startRot);
        }
    }

}
