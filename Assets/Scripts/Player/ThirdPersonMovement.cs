using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float speed = 6f;
    [SerializeField] CinemachineFreeLook cameraClose;
    [SerializeField] GameObject Inventory;
    CharacterController controller;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector2 vertVel;
    float jumpForce = 5f;
    float gravity = 20f;
    float targetAngle;
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                vertVel.y = jumpForce;
        }
        else
        {
            vertVel.y -= gravity * Time.deltaTime;
        }

        controller.Move(vertVel * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            //cameraClose.transform.rotation = cam.rotation;
            //cameraClose.transform.position = cam.position;
            cameraClose.Priority = 2;
            targetAngle = cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            cameraClose.Priority = 1;
        }

        if(direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDir = Camera.main.transform.TransformDirection(direction);
                controller.Move(moveDir * Time.deltaTime);
            }
            else 
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

        }
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        return (Physics.Raycast(ray, 0.3f));
    }
}
