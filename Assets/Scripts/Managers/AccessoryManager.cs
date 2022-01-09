using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    CurrentItem currentItem;
    [SerializeField] GameObject[] Weapons;
    int switchCheck;
    GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        currentItem = GameObject.FindGameObjectWithTag("Manager").GetComponent<CurrentItem>();
        switchCheck = currentItem.arrayPos;
        currentObj = Instantiate(Weapons[switchCheck], gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Weapons.Length-1 < currentItem.arrayPos)
            currentItem.arrayPos = 0;
        if (switchCheck != currentItem.arrayPos)
            UpdateItem();
    }

    void UpdateItem()
    {
        Destroy(currentObj);
        switchCheck = currentItem.arrayPos;
        print(switchCheck);
        currentObj = Instantiate(Weapons[switchCheck], gameObject.transform);
    }
}
