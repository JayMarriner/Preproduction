using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGen : MonoBehaviour
{
    [SerializeField] GameObject spike;
    [SerializeField] GameObject spikeFloor;
    [SerializeField] float spikeDensity;
    Vector2 pitSize;

    // Start is called before the first frame update
    void Start()
    {
        if (spikeDensity == 0)
            spikeDensity = 0.5f;

        pitSize = new Vector2(spikeFloor.transform.localScale.x, spikeFloor.transform.localScale.z);
        float counter = 0;
        while (counter <= pitSize.x)
        {
            float zCounter = 0;
            while (zCounter <= pitSize.y)
            {
                GameObject newSpike= Instantiate(spike, transform);
                newSpike.transform.localScale = new Vector3(1, 1, 1);
                newSpike.transform.localPosition = new Vector3((counter - pitSize.x/2) , 0, -pitSize.y/2 + zCounter);
                zCounter+= spikeDensity;
            }
            counter+= spikeDensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
