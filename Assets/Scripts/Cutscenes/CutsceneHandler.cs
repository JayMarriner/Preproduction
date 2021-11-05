using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneHandler : MonoBehaviour
{
    CinemachineTrackedDolly dolly;
    [SerializeField] float cutsceneSpeed = 1;
    public bool playCutscene;
    [SerializeField] Camera cam;
    [SerializeField] float endTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        dolly = GetComponentInChildren<CinemachineTrackedDolly>();
        cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playCutscene)
        {
            cam.enabled = true;
            dolly.m_PathPosition += Time.deltaTime * cutsceneSpeed / 10;
            if (dolly.m_PathPosition >= 1f)
            {
                StartCoroutine(EndCutscene());
            }
        }
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(endTime);
        playCutscene = false;
        cam.enabled = false;
        gameObject.SetActive(false);
    }
}
