using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target, Player, Weapon;
    public Camera camera;
    public float Sensitivity;
    private float mouseX, mouseY;

    private float maxFov = 100f;
    private float minFov = 60f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        CameraZoom();
    }

    void LateUpdate()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * Sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Sensitivity;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            //transform.LookAt(Weapon.transform.position);
        }
        else
            transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);             
                
    }

    void CameraZoom()
    {
        //Camera Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera.fieldOfView < maxFov)
        {
            camera.fieldOfView++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.fieldOfView > minFov)
        {
            camera.fieldOfView--;
        }
        else
            camera.fieldOfView = camera.fieldOfView;
        
    }
}
