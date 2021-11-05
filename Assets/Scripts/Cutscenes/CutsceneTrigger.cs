using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject cutsceneManager;
    GameManager gm;
    CutsceneHandler cHandler;
    bool cutsceneStarted;
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        cHandler = cutsceneManager.GetComponent<CutsceneHandler>();
        cutsceneManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneStarted)
        {
            if (!cHandler.playCutscene)
            {
                mainCam.enabled = true;
                gm.currentState = GameManager.GameState.Play;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cutsceneManager.SetActive(true);
            mainCam = Camera.main;
            mainCam.enabled = false;
            cHandler.playCutscene = true;
            gm.currentState = GameManager.GameState.Cutscene;
            cutsceneStarted = true;
        }
    }
}
