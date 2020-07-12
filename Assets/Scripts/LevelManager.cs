using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _inputsAmount;
    [SerializeField] private Slider _commandsSlider;
    private PlayerController _player;
    private Vector3 _initialPlayerPosition;
    private Queue<InputData> _playerInputs;
    [Tooltip("called when the turn for the player to pick his actions ends")]
    public UnityEngine.Events.UnityEvent OnTurnSwitch;
    public bool PlayerTurn{get; private set;}
    public int MaxInputs => _inputsAmount;
    public int CurrentInputs => _playerInputs.Count;
    public float Percent => (float)CurrentInputs / (float)MaxInputs;
    public Queue<InputData> PlayerInputs
    {
        get
        {
            return _playerInputs;
        }
    }

    public int InputsAmount 
    {
        get
        {
            return _inputsAmount;
        }
        set
        {
            _inputsAmount = value;
            if (_inputsAmount <= 0)
            {
                PlayerTurn = false;
                OnTurnSwitch?.Invoke();
            }
        }
    }

    private void Awake() 
    {
        _playerInputs = new Queue<InputData>(_inputsAmount);
    }

    private void Start() 
    {
        PlayerTurn = true;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _initialPlayerPosition = _player.transform.position;
    }

    private void Update() 
    {
        _commandsSlider.SetValueWithoutNotify(Percent);
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            RunLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetLevel();
        }
    }

    public void RunLevel()
    {
        if (CurrentInputs < InputsAmount) return;

        _player.Run();
    }

    public void ResetLevel()
    {
        _player.transform.position = _initialPlayerPosition;
        _playerInputs.Clear();
        onLevelReset?.Invoke();
    }

    public event Action onLevelReset;
}
