using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Dynamic")]
    public Vector3 camPos;

    void Awake()
    {
        camPos = this.transform.position;
       
    }
    
    void FixedUpdate()
    {
        if(POI == null)
        {
            return;
        }

        Vector3 dest = POI.transform.position;
        if (dest.x < camPos.x)
        {
            dest.x = camPos.x;
        }
        if (dest.y < camPos.y)
        {
            dest.y = camPos.y;
        }
        dest.z = camPos.z;
        transform.position = dest;
    }
}
