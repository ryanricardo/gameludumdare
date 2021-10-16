using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    public      Transform       target = null;
    [HideInInspector]   private     Vector3         offset;
                        private     Vector3         camStartPos;

    [Header("Atributtes Camera")]
    [SerializeField]    private     float           stopPos;
    [SerializeField]    private     float           followDelay = 4;

   

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
        camStartPos = transform.position;
    }

    // Camera Updates should be done in LateUpdate
    void LateUpdate()
    {
        if(target == null)
        return;

        if(target.position.x >= camStartPos.x){
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followDelay);     
        }
        if(transform.position.x >= stopPos){
            transform.position = new Vector3(stopPos, transform.position.y, transform.position.z);
        }
        if(target.position.x < camStartPos.x){
            Vector3 newPos = transform.position;
            newPos.x = camStartPos.x;
            transform.position = newPos;
        }

        // transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followDelay);         
        // if(transform.position.x > stopPos){
        //     transform.position = new Vector3(stopPos, transform.position.y, transform.position.z);
        // }
        // if(target.position.y < -3){
        //     transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        // }
    }
}
