using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackHandler : MonoBehaviour
{
    [SerializeField] float fuel;
    ThirdPersonPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        fuel = 4f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            player.Flying();
        }
    }
}
