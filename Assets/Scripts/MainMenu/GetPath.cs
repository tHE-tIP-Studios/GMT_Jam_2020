using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    private const string boringPath = "/AppData/LocalLow/";
    private const string notSoBoringPath = "/no_{0}/control_{1}/";
    private void Awake()
    {
        string text = Application.persistentDataPath;
        text = text.Replace(boringPath, string.Format(notSoBoringPath, Random.Range(0, 9), Random.Range(10, 999)));
        GetComponent<TextMeshProUGUI>().text = text;
    }
}