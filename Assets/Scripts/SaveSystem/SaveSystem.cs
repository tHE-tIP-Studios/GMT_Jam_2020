using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSystem
{
    private static SaveSystem _current;
    public static SaveSystem Current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveSystem();
                _current.Levels = new Dictionary<int, bool>();
            }
            return _current;
        }

        set
        {
            _current = value;
        }
    }

    public Dictionary<int, bool> Levels;
}
