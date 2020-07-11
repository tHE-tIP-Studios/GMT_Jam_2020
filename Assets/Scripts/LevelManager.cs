using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _inputsAmount;
    [SerializeField] private Slider _commandsSlider; 
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

    private void Awake() {
        _playerInputs = new Queue<InputData>(_inputsAmount);
    }

    private void Start() {
        PlayerTurn = true;
    }

    private void Update() {
        _commandsSlider.SetValueWithoutNotify(Percent);
    }
}
