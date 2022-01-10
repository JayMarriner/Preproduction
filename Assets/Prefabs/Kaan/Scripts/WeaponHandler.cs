using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    void Start()
    {
        if (muzzleFlash.isPlaying)
            muzzleFlash.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash.Play();
        }        
    }
}
