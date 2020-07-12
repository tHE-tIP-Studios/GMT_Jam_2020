using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, IClimbable
{
    public int Length {get;}
    private LineHandler _line;
    private void Start()
    {
        _line = GetComponentInChildren<LineHandler>();
        BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();

        collider2D.size = new Vector2(.8f, _line.LineLenght);
        collider2D.offset = new Vector2(0.0f, -_line.LineLenght/2);
    }

    private void FixedUpdate() 
    {
  
    }
    
}
