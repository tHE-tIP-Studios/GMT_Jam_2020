using UnityEngine;
using System;

public struct InputData
{
    public InputType Type{get;}
    public Vector3 MoveAmount {get;}
    public float TimeBefore {get;}
    public Action onInput;

    public InputData(Vector3 move, float time, InputType type)
    {
        MoveAmount = move;
        TimeBefore = time;
        Type = type;
        onInput = null;
    }

    public InputData(float time, Action inputEvent, InputType type)
    {
        Type = type;
        MoveAmount = Vector3.zero;
        TimeBefore = time;
        onInput = inputEvent;
    }

    public InputData(Vector3 move, float time, Action inputEvent, InputType type)
    {
        Type = type;
        MoveAmount = move;
        TimeBefore = time;
        onInput = inputEvent;
    }
}