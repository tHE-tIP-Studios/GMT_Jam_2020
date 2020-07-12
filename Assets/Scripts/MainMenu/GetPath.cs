using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GetPath : MonoBehaviour
{
    private const string boringPath = "/AppData/LocalLow/";
    private const string notSoBoringPath = "/no_{0}/control_{1}/";
    string _finalTxt;
    TextMeshProUGUI _txtPro;
    private Coroutine cortn;
    private void Awake()
    {
        _finalTxt = Application.persistentDataPath;
        _finalTxt = _finalTxt.Replace(boringPath, string.Format(notSoBoringPath, Random.Range(0, 9), Random.Range(10, 999)));

        _txtPro = GetComponent<TextMeshProUGUI>();
        _txtPro.text = "";
        cortn = StartCoroutine(Write());
    }

    private IEnumerator Write(int index = 0)
    {
        _txtPro.text += _finalTxt[index];
        yield return new WaitForSeconds(0.1f);
        if (index + 1 < _finalTxt.Length)
            cortn = StartCoroutine(Write(index + 1));
    }

    private void OnDisable()
    {
        if (cortn != null)
            StopCoroutine(cortn);
    }
}