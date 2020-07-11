using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool PlayerTurn{get; private set;}
    
    private Queue<InputData> _playerInputs;

    public Queue<InputData> PlayerInputs
    {
        get
        {
            if (_playerInputs == null) _playerInputs = new Queue<InputData>();
            return _playerInputs;
        }
    }

    public int InputsAmount {get; set;}
}
