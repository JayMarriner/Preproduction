using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCameraSwitch : MonoBehaviour
{
    //Cameras
    [SerializeField] private Transform mainCameraPos;
    [SerializeField] private Transform aimCameraPos;

    //UI
    [SerializeField] private GameObject crosshair;

    //Vars
    private bool isAiming;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Camera.main.transform.position = Vector3.Lerp(mainCameraPos.position, aimCameraPos.position, 5f * Time.deltaTime);
            isAiming = false;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Camera.main.transform.position = Vector3.Lerp(aimCameraPos.position, mainCameraPos.position, 5f * Time.deltaTime);
            isAiming = true;
        }
        

        if (isAiming)
        {
            crosshair.SetActive(true);
        }
        else
            crosshair.SetActive(false);
    }
       
}
