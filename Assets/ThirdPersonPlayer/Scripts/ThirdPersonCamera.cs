using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform Player { get; set; }
    public Transform CentrePoint { get; set; }
    
    private float mouseX, mouseY;

    GameManager gm;

    Canvas canva;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canva = GetComponentInChildren<Canvas>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gm.currentState == GameManager.GameState.Cutscene)
        {
            canva.enabled = false;
        }

        if(canva.enabled == false && gm.currentState == GameManager.GameState.Play)
        {
            canva.enabled = true;
        }

        if (Player == null)
            Destroy(gameObject);

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        CentrePoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0f);

        /*ADDED*/
        transform.position = Player.transform.position;
    }

    /*private void LateUpdate()
    {
        transform.position = Player.transform.position;
    }*/
}
