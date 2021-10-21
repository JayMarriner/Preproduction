using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] GameObject leftClick;
    [SerializeField] GameObject rightClick;

    // Start is called before the first frame update
    void Start()
    {
        leftClick.SetActive(false);
        rightClick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            leftClick.SetActive(true);
        else
            leftClick.SetActive(false);

        if (Input.GetKey(KeyCode.Mouse1))
            rightClick.SetActive(true);
        else
            rightClick.SetActive(false);
    }
}
