using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;

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

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = launchPoint.transform.position;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
