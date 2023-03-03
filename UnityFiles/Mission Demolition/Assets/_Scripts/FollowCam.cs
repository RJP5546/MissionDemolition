using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S; //Another private singleton
    static public GameObject POI; //Static point of interest

    public enum eView { none, slingshot, castle, both};

    [Header("Inscribed")]
    public GameObject viewBothGO;

    [Header("Dynamic")]
    public Vector3 camPos;
    public eView nextView = eView.slingshot;

    void Awake()
    {
        S = this;
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

    public void SwitchView (eView newView)
    {
        if (newView == eView.none)
        {
            newView = nextView;
        }
        switch (newView)
        {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                nextView = eView.slingshot;
                break;
        }
    }

    public void SwitchView()
    {
        SwitchView(eView.none);
    }

    static public void SWITCH_VIEW(eView newView)
    {
        S.SwitchView(newView);
    }
}
