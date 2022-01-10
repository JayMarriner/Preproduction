using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    ThirdPersonPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        player.canDoubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
