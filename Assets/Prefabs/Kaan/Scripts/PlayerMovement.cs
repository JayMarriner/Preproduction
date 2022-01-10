using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //UI
    [SerializeField] private GameObject jetpackElements;

    //Portal Logic 
    [SerializeField] private GameObject portalA, portalB;

    public Jetpack Jetpack { get; private set; }

    public CharacterController controller;
          

    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    Vector3 vertVel;

    private Vector3 moveDirection = Vector3.zero;

    public bool usingJetpack;

    private void Start()
    {
        jetpackElements.SetActive(false);
        Jetpack = GetComponentInChildren<Jetpack>();
        Jetpack.gameObject.SetActive(false);              
        
    }

    bool IsGrounded()
    {
        //Sends a ray to the ground to check if the player is grounded.
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down); 
        //Gets the ray and controls the threshold of the ray.
        return (Physics.Raycast(ray, 0.3f)); 
    }

    void Update()
    {
        //Updates bool when jetpack is false. 
        if (!Jetpack.gameObject.activeSelf && usingJetpack)
            usingJetpack = false;

        //Gets the movement axis.
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); 

        if (IsGrounded())
        {
            if (Input.GetButton("Jump"))
                vertVel.y = jumpSpeed;
        }
        else
        {
            if (!usingJetpack)
                vertVel.y -= gravity * Time.deltaTime;
        }

        controller.Move(vertVel * Time.deltaTime);

       if (direction.magnitude >= 0.1f)
       { 
            //Determine rotation angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;

            //Construct a movement vector using angle and normalised direction
            moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) * direction.sqrMagnitude;
                       
       }
       //Moves the controller. 
        controller.Move(moveDirection * speed * Time.deltaTime);

        JetpackStatus();

        ToggleJetpack();
    }

    void JetpackStatus()
    {
        if (Jetpack.jetpackEquipped)
        {
            Jetpack.gameObject.SetActive(true);
            Jetpack.effect.Stop();
            Jetpack.effect1.Stop();
            jetpackElements.SetActive(true);
        }
            
    }

    void ToggleJetpack() //Allows for the player to enable and disable the use of the jetpack.
    {
        //Checks for the input and whether the player has collected the jetpack. 
        if (Input.GetKeyDown(KeyCode.X) && Jetpack.HasJetPack)
        {
            //Allows for the input to both activate and de-activate the jetpack.
            Jetpack.jetpackEquipped = !Jetpack.jetpackEquipped;

            //Sets the jet pack to active or inactive based on the equipped bool.
            Jetpack.gameObject.SetActive(Jetpack.jetpackEquipped);

            //Sets the canvas to inactive or active based on the players input.
            if (jetpackElements.activeSelf)
            {
                jetpackElements.SetActive(false);
            }
            
        }
       
    }

    public void Portal()
    {       
        if (transform.position == portalA.transform.position)
        {
            transform.position = portalB.transform.position;
        }
        else if (transform.position == portalB.transform.position)
        {
            transform.position = portalA.transform.position;
        }
        else
            transform.position = transform.position;
    }

}
