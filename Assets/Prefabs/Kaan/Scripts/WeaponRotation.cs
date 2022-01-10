using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Gun;
    [SerializeField] private Camera Cam;
    private Vector3 targetPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            AimGun();
        }      
        else
        {
            Gun.localEulerAngles = Vector3.zero;
        }
    }       

    void AimGun()
    {
        targetPoint = Cam.transform.forward * 100000f;
        Gun.LookAt(targetPoint);
    }

}
