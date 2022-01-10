using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    private PlayerMovement playerScript;

    //UI
    public JetpackFuel JetpackFuel;    

    //Vars
    [SerializeField] private float maxFuel = 4f;
    [SerializeField] private float currFuel;
  
    //Particle Effects
    public ParticleSystem effect;
    public ParticleSystem effect1;

    public bool HasJetPack { get; set; }
    public bool jetpackEquipped = false;

    void Start()
    {
        currFuel = maxFuel;

        //UI
        JetpackFuel.SetMaxFuel(maxFuel);

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        effect.Stop();
        effect1.Stop();
    }

    void Update()
    {
        //UI
        JetpackFuel.GetJetpackFuel(currFuel); 

        Cursor.lockState = CursorLockMode.Locked;

        if (jetpackEquipped)
        {
            if (Input.GetKey(KeyCode.Space) && currFuel > 0f)
            {
                playerScript.usingJetpack = true;
                currFuel -= Time.deltaTime;
                effect.Play();
                effect1.Play();                
            }

            if (Input.GetKeyUp(KeyCode.Space) || currFuel <= 0f)
            {
                playerScript.usingJetpack = false;
                effect.Stop();
                effect1.Stop();
            }

            if (playerScript.controller.isGrounded)
                StartCoroutine(Refuel());
        }            

        
    }

    //This allows for a period of time to pass before the jetpacks fuel can begin to replenish.
    IEnumerator Refuel() 
    {
        while(currFuel < maxFuel) 
        {
            currFuel += 1f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
