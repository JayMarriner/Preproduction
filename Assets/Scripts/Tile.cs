using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    [ExecuteInEditMode]
    // Update is called once per frame
    void Update()
    {
        //Store the x and y scales of the attached object.
        Vector2 uvScale = new Vector2(transform.localScale.x, transform.localScale.y);

        //Set the texture to the scale of the object.
        renderer.material.SetTextureScale("_MainTex", uvScale);
    }
}
