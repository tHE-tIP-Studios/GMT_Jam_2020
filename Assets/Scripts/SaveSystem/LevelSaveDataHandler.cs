using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaveDataHandler : MonoBehaviour
{
    private LevelSelect _levelSelected;
    private void Awake() 
    {
        _levelSelected = GetComponent<LevelSelect>();    
    }
}
