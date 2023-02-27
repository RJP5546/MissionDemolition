using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        launchPoint = transform.Find("LaunchPoint").gameObject;
        launchPoint.SetActive(false);
        aimingMode = false;


    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PRE-Condition: if aiming mode, then projectile is instantiated
        if (!aimingMode)
        {
            return;
        }

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPoint.transform.position;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPoint.transform.position + mouseDelta;
        //PRE-Condition: projectile needs to be instantiated
        projectile.transform.position = projPos;

        //POST-CONDITION: when !aimingMode, launchPoint.SetAcitve(false)
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            launchPoint.SetActive(false);
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.isKinematic = false;
            projRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRb.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }
    }

    void OnMouseEnter()
    {
        //print("Slingshot::OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Slingshot::OnMouseExit()");
        if (!aimingMode)
        {
            launchPoint.SetActive(false);
        }

        
    }

    void OnMouseDown()
    {
        aimingMode = true; //POST-CONDITION: projectile must be intantiated
        projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = launchPoint.transform.position;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
