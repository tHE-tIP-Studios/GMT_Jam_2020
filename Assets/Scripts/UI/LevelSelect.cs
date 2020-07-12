using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private int _level;
    private UnityEngine.UI.Button _border;
    public bool Completed;
    public int Level => _level;
    
    private void Awake() 
    {
        _border = GetComponent<UnityEngine.UI.Button>();
        GetComponentInChildren<TextMeshProUGUI>().text = _level.ToString();
        if (Completed)
        {
            _border.enabled = true;
        }
        else
        {
            _border.enabled = false;
            GetComponent<Image>().color = _border.colors.disabledColor;
        }
    }

    public void SelectLevel()
    {
        SceneLoader.Load(_level.ToString());
    }
}
