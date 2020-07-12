using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LoadingLine : MonoBehaviour
{
    private readonly Vector2 _delayRange = new Vector2(0.1f, 0.3f);
    private readonly char[] _sequence = new char[8]
    {
        '|',
        '/',
        '-',
        '\\',
        '|',
        '/',
        '-',
        '\\'
    };
    private TextMeshProUGUI _linePro;

    private int _currIndex;
    private Coroutine _crtn;

    private void Awake()
    {
        _linePro = GetComponent<TextMeshProUGUI>();
        _crtn = StartCoroutine(CBar());
    }

    private IEnumerator CBar()
    {
        UpdateText();
        yield return new WaitForSeconds(Random.Range(_delayRange.x, _delayRange.y));
        if (++_currIndex >= _sequence.Length)
            _currIndex = 0;
        _crtn = StartCoroutine(CBar());
    }

    private void UpdateText()
    {
        _linePro.text = _sequence[_currIndex].ToString();
    }

    private void OnDisable()
    {
        if (_crtn != null)
            StopCoroutine(_crtn);
    }
}