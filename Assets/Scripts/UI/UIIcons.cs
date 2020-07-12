using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI")]
public class UIIcons : ScriptableObject
{
    public Sprite Left;
    public Sprite Right;
    public Sprite Up;
    public Sprite Down;
    public Sprite Pause;
    public Sprite Interact;

    [Header("If we have time for controller stuff")]
    // If we have time for controller stuff
    public Sprite CLeft;
    public Sprite CRight;
    public Sprite CUp;
    public Sprite CDown;
    public Sprite CPause;
    public Sprite CInteract;

}
