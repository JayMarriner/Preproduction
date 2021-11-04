using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play,
        Paused,
        Menu,
        Cutscene
    }

    public GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GameState.Play)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
