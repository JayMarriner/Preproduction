using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWheel : MonoBehaviour
{
    CurrentItem currentItem;
    [SerializeField] Image currentImg;

    // Start is called before the first frame update
    void Start()
    {
        currentItem = GameObject.FindGameObjectWithTag("Manager").GetComponent<CurrentItem>();
    }

    // Update is called once per frame
    void Update()
    {
        currentImg.sprite = currentItem.ReturnCurrentImg();
    }
}
