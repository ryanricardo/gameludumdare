/*
 * CameraController.cs
 * 
 * 22/10/21 - dga
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;

    [SerializeField] Transform TargetObj;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 pos;
    [SerializeField] float spd;
    [SerializeField] float freespace;

    void Awake()
    {
        pos = TargetObj.position;
        transform.position = TargetObj.position + offset;
    }

    [SerializeField] Vector3 target;
    void Update()
    {
        target = TargetObj.position;

        if(pos.x > target.x - offset.x)
          pos.x -= spd * Time.deltaTime;
        if(pos.x < target.x + offset.x)
          pos.x += spd * Time.deltaTime;

        if(pos.y > target.y - offset.y)
          pos.y -= spd * Time.deltaTime;
        if(pos.y < target.y + offset.y)
          pos.y += spd * Time.deltaTime;

        if(pos.x < minPos.x)
          pos.x = minPos.x;
        else if(pos.x > maxPos.x)
          pos.x = maxPos.x;

        if(pos.y < minPos.y)
          pos.y = minPos.y;
        else if(pos.y > maxPos.y)
          pos.y = maxPos.y;

        transform.position = new Vector3(
            pos.x, 
            pos.y, 
            transform.position.z
            );
    }
}
