using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _inputsAmount;
    private Queue<InputData> _playerInputs;
    [Tooltip("called when the turn for the player to pick his actions ends")]
    public UnityEngine.Events.UnityEvent OnTurnSwitch;
    public bool PlayerTurn{get; private set;}
    public int MaxInputs => _inputsAmount;
    public int CurrentInputs => _playerInputs.Count;
    public float Percent => CurrentInputs / MaxInputs;
    public Queue<InputData> PlayerInputs
    {
        get
        {
            if (_playerInputs == null) _playerInputs = new Queue<InputData>(_inputsAmount);
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

    private void Start() {
        PlayerTurn = true;
    }
}
