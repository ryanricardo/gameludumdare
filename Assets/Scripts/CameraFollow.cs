using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;
    [SerializeField] float stopPos;
    [SerializeField] private float followDelay = 4;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Camera Updates should be done in LateUpdate
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followDelay);         
        if(transform.position.x > stopPos){
            transform.position = new Vector3(stopPos, transform.position.y, transform.position.z);
        }
        if(target.position.y < -3){
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
