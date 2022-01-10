using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackHandler : MonoBehaviour
{
    [SerializeField] float fuel;
    [SerializeField] Image tankImg;
    float maxFuel;
    ThirdPersonPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        fuel = 2f;
        maxFuel = fuel;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0f)
        {
            player.usingJetpack = true;
            fuel -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || fuel <= 0f)
        {
            player.usingJetpack = false;
        }

        if (player.onGround && fuel < maxFuel)
            fuel += Time.deltaTime;
        if (fuel > maxFuel)
            fuel = maxFuel;

        tankImg.fillAmount = fuel / maxFuel;
    }
}
