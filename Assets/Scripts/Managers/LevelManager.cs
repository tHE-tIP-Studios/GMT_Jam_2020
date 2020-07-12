using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _inputsAmount = default;
    [SerializeField] private PlayerController _playerPrefab = default;
    [SerializeField] private Transform _levelSpawn = default;
    [SerializeField] private InputUI _inputUIPrefab = default;
    [SerializeField] private RectTransform _inputPanel = default;
    public PlayerController CurrentPlayer { get; private set; }
    private Queue<InputData> _playerInputs;
    private int _currentComand;
    public static LevelManager Instance { get; private set; }
    [Tooltip("called when the turn for the player to pick his actions ends")]
    public UnityEngine.Events.UnityEvent OnTurnSwitch;
    public bool PlayerTurn { get; private set; }
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
        StartCoroutine(WaitBeforeSettingAwake());
    }

    private void Start()
    {
        StartCoroutine(WaitBeforeSettingStart());
    }

    private void NewInputUI(InputData data)
    {
        InputUI ui = Instantiate(_inputUIPrefab, _inputPanel);
        ui.SetInfo(data);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            RunLevel();
        }

        if (Input.GetKeyDown(KeyCode.R) && !PauseControl.Active)
        {
            ResetLevel();
        }
    }

    private IEnumerator WaitBeforeSettingAwake()
    {
        yield return new WaitForSeconds(2f);
        _playerInputs = new Queue<InputData>(_inputsAmount);
        Instance = this;
    }
    private IEnumerator WaitBeforeSettingStart()
    {
        yield return new WaitForSeconds(2f);
        PlayerTurn = true;
        CurrentPlayer = Instantiate(_playerPrefab, _levelSpawn.position, _levelSpawn.rotation);
        CurrentPlayer.onNewInput += NewInputUI;
        CurrentPlayer.onNewNextInput += RunUI;
    }

    /// <summary>
    /// Call to start the player movement
    /// </summary>
    public void RunLevel()
    {
        if (CurrentInputs < InputsAmount) return;
        CurrentPlayer.Run();
    }

    private void RunUI()
    {
        InputUI ui;
        ui = _inputPanel.GetChild(_currentComand).GetComponent<InputUI>();
        ui.StartCountdown();
        _currentComand++;
    }

    /// <summary>
    /// Call this when the player dies or he cant reach the goal in the number of actions
    /// </summary>
    public void ResetLevel()
    {
        CurrentPlayer.onNewInput -= NewInputUI;
        CurrentPlayer.onNewNextInput -= RunUI;

        foreach (Transform t in _inputPanel.transform)
        {
            Destroy(t.gameObject);
        }

        CurrentPlayer.StopAllCoroutines();
        Destroy(CurrentPlayer.gameObject);
        CurrentPlayer = null;
        PlayerController tempPlayer = Instantiate(_playerPrefab, _levelSpawn.position, _levelSpawn.rotation);
        CurrentPlayer = tempPlayer;
        CurrentPlayer.onNewInput += NewInputUI;
        CurrentPlayer.onNewNextInput += RunUI;

        _playerInputs.Clear();
        _currentComand = 0;

        onLevelReset?.Invoke();
    }

    public event Action onLevelReset;
}
