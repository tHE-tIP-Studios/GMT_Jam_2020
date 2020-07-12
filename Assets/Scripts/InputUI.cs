using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Image _timeImage;
    private TextMeshProUGUI _timeText;

    private void Awake() 
    {
        _timeText = _timeImage.GetComponentInChildren<TextMeshProUGUI>();    
    }

    
}
