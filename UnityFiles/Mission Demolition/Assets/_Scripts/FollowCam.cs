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

        Vector3 dest = Vector3.zero;

        if (POI != null)
        {
            //Checks if POI has a rigidbody and sees if it is sleeping.
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping() )
            {
                POI = null;
            }
        }

        if (POI != null)
        {
            dest = POI.transform.position;
        }

        if (dest.x < camPos.x)
        {
            dest.x = camPos.x;
        }
        if (dest.y < camPos.y)
        {
            dest.y = camPos.y;
        }
        dest.z = camPos.z;

        //set camera to destination
        transform.position = dest;
        //set orthographicSize of the Camera to keep the Ground in view
        Camera.main.orthographicSize = dest.y + 10;
        

    }
}
