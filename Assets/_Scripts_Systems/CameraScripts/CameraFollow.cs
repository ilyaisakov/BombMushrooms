using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 target_Offset;

    private void Awake()
    {
        //get offset
       target_Offset.x = transform.position.x + (target.position.x * -1);
       target_Offset.y = transform.position.y + target.position.y;
       target_Offset.z = transform.position.z + (target.position.z * -1);
    }
    void LateUpdate()
    {
        if (target)
        {
            //smooth
            transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, 0.1f);
        }
    }
}
//without Smooth
//    private Vector3 myPos;
//        myPos = transform.position;
    //transform.position = target.position + myPos;