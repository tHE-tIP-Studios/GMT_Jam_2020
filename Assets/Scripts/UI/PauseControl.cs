using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private GameObject _pauseElements = default;

    public static bool Active {get; private set;}

    private void Update() 
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
            LevelManager.Instance
                .CurrentPlayer.AddNewInput(TogglePause, KeyCode.P);
        }
    }

    public void Resume()
    {
        TogglePause();
        LevelManager.Instance
            .CurrentPlayer.AddNewInput(TogglePause, KeyCode.P);
    }

    public void TogglePause()
    {
        Active = !Active;
        _pauseElements.SetActive(Active);
    }

    private void DisablePause()
    {
        _pauseElements.SetActive(false);
    }
}
