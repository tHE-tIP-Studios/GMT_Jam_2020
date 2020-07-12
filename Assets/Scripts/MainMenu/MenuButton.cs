using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private char _selectionPrefix = '>';
    [SerializeField] private TextMeshProUGUI _textPro = default;

    private EventTrigger _eventTrigger;

    public void OnSelected()
    {
        string text = _textPro.text;
        text = _selectionPrefix + text;
        _textPro.text = text;
    }
    public void OnDeSelected()
    {
        string text = _textPro.text.TrimStart(_selectionPrefix);
        _textPro.text = text;
    }
}