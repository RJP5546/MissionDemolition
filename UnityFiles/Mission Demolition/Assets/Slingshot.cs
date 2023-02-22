using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject launchPoint;

    void Awake()
    {
        //Transform launchPointTrans = transform.Find("LaunchPoint");
        //launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        //print("Slingshot::OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot::OnMouseExit()");
        launchPoint.SetActive(false);
    }
}
