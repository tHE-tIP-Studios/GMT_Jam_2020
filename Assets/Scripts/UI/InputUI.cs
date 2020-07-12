using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Image _timeImage = default;
    [SerializeField] private Image _inputImage = default;
    [SerializeField] private GameObject _innactiveImage = default;
    [SerializeField] private UIIcons _uiInfo = default;
    private TextMeshProUGUI _timeText;
    private InputData _inputInfo;
    private void Awake()
    {
        _timeText = _timeImage.GetComponentInChildren<TextMeshProUGUI>(); 
    }

    public void SetInfo(InputData data)
    {
        _inputInfo = data;
        _timeText.text = $"{_inputInfo.TimeBefore.ToString("#.##")} s";;
        // Set the correct UI
        switch(data.Key)
        {
            case KeyCode.LeftArrow:
                _inputImage.sprite = _uiInfo.Left;
                break;
            case KeyCode.RightArrow:
                _inputImage.sprite = _uiInfo.Right;
                break;
            case KeyCode.UpArrow:
                _inputImage.sprite = _uiInfo.Up;
                break;
            case KeyCode.DownArrow:
                _inputImage.sprite = _uiInfo.Down;
                break;
            case KeyCode.E:
                _inputImage.sprite = _uiInfo.Interact;
                break;
            case KeyCode.P:
                _inputImage.sprite = _uiInfo.Pause;
                break;
        }
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float time = _inputInfo.TimeBefore;
        while(time > 0)
        {
            time -= Time.deltaTime;
            // Set the ui image fill amount
            _timeImage.fillAmount = time / _inputInfo.TimeBefore;
            _timeText.text = $"{time.ToString("#.##")} s";
            yield return null;
        }
        
        _timeImage.fillAmount = 0.0f;
        _timeText.text = $"0 s";

        _innactiveImage.SetActive(true);
    }
}
