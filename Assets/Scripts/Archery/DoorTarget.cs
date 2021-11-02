using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour
{
    [SerializeField] DoorHandler door;

    public void TargetHit()
    {
        //Door set in unity, run code on door.
        door.OpenDoor();
    }
}
