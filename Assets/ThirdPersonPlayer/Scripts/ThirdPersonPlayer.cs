using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private GameObject cameraRig;

    private CharacterController controller;
    private GameObject cameraRigInst;
    private Transform cam;
    private float speed = 5f;
    private Vector3 vertVel;
    private float gravity = 15f;
    private float jumpForce = 5f;
    private float rotationSmooth = 0.1f;
    private float turnSmoothVelocity;
    Vector3 moveDirection;
    public bool onGround;
    public bool canDoubleJump;
    public bool itemSwitched;
    public bool usingJetpack;
    public bool alreadyDoubleJump;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        InitialiseCamera();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (itemSwitched)
        {
            usingJetpack = false;
            canDoubleJump = false;
            itemSwitched = false;
        }

        if (usingJetpack)
        {
            vertVel.y = jumpForce/1.5f;
        }

        //Calculate vertical movement first
        if (IsGrounded())
        {
            onGround = true;
            if (Input.GetKeyDown(KeyCode.Space))
                vertVel.y = jumpForce;
            if (alreadyDoubleJump)
                alreadyDoubleJump = false;
        }
        else
        {
            if(canDoubleJump && !alreadyDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    vertVel.y = jumpForce;
                    alreadyDoubleJump = true;
                }
            }
            onGround = false;
            if(!usingJetpack)
                vertVel.y -= gravity * Time.deltaTime;
        }

        controller.Move(vertVel * Time.deltaTime);

        //Calculate 2D movement next
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(hor, 0f, ver);

        if (direction.magnitude >= 0.1f)
        {
            //Determine rotation angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //Smooth between current and target angle
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);

            if (Input.GetKey(KeyCode.Mouse1))
            {
                //Apply rotation to the transform
                transform.rotation = Camera.main.transform.rotation;
            }
            else
            {
                //Apply rotation to the transform
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            }

            //If direction magnitude greater than 1 (through strafing/lateral movement), then normalise it
            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            //Construct a movement vector using angle and normalised direction
            moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) * direction.sqrMagnitude;

            //Apply movement to the character controller
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            //Apply rotation to the transform
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        return (Physics.Raycast(ray, 0.3f));
    }

    void InitialiseCamera()
    {
        cameraRigInst = Instantiate(cameraRig);
        cameraRigInst.name = cameraRig.name;
        ThirdPersonCamera camScript = cameraRigInst.GetComponent<ThirdPersonCamera>();
        camScript.Player = transform;
        camScript.CentrePoint = cameraRigInst.transform;
        cam = camScript.GetComponentInChildren<Camera>().transform;
    }
}
