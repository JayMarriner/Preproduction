using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform Player { get; set; }
    public Transform CentrePoint { get; set; }
    
    private float mouseX, mouseY;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            Destroy(gameObject);

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        CentrePoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0f);        
    }

    private void LateUpdate()
    {
        transform.position = Player.transform.position;
    }
}
