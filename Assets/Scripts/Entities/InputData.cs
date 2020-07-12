using UnityEngine;
using System;

public struct InputData
{
    public InputType Type{get;}
    public KeyCode Key {get;}
    public Vector3 MoveAmount {get;}
    public float TimeBefore {get;}
    public Action onInput;

    public InputData(Vector3 move, float time, InputType type, KeyCode key)
    {
        MoveAmount = move;
        TimeBefore = time;
        Type = type;
        onInput = null;
        Key = key;
    }

    public InputData(float time, Action inputEvent, InputType type, KeyCode key)
    {
        Type = type;
        MoveAmount = Vector3.zero;
        TimeBefore = time;
        onInput = inputEvent;
        Key = key;
    }

    public InputData(Vector3 move, float time, Action inputEvent, InputType type, KeyCode key)
    {
        Type = type;
        MoveAmount = move;
        TimeBefore = time;
        onInput = inputEvent;
        Key = key;
    }
}