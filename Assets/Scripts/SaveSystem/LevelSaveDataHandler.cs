using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaveDataHandler : MonoBehaviour
{
    private LevelSelect _levelSelected;
    private void Awake() 
    {
        _levelSelected = GetComponent<LevelSelect>();
        if (SaveSystem.Current.Levels[_levelSelected.Level])
        {
            _levelSelected.Completed = true;
        }
        else
        {
            _levelSelected.Completed = false;
        }
    }
}
