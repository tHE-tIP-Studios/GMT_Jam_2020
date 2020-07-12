using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private GameObject _pauseElements = default;

    private LevelManager _manager;
    private bool _active;
    
    private void Awake() 
    {
        _manager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();    
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
            _manager.CurrentPlayer.AddNewInput(TogglePause, KeyCode.P);
        }    
    }

    public void Resume()
    {
        TogglePause();
        _manager.CurrentPlayer.AddNewInput(TogglePause, KeyCode.P);
    }

    public void TogglePause()
    {
        _active = !_active;
        _pauseElements.SetActive(_active);
    }
}
