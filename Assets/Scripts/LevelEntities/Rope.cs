using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, IClimbable
{
    public int Length {get;}
    private LineRenderer _line;
    private CompositeCollider2D _composite;
    private CircleCollider2D[] _colliders;
    private void Start()
    {
        _composite = GetComponent<CompositeCollider2D>();
        _line = GetComponentInChildren<LineRenderer>();
        CircleCollider2D collider2D;
        for (int i = 0; i < _line.positionCount; i++)
        {
            GameObject collider = new GameObject("RopeCollider" + i);
            collider.layer = LayerMask.NameToLayer("Climbables");
            collider.transform.parent = transform;
            collider.transform.position = _line.GetPosition(i);
            collider2D = collider.AddComponent<CircleCollider2D>();
            collider2D.radius = .4f;
        }
        _colliders = GetComponentsInChildren<CircleCollider2D>();
    }

    private void FixedUpdate() 
    {
        for (int i = 0; i < _line.positionCount; i++)
        {
            _colliders[i].transform.position = _line.GetPosition(i);
        }    
    }
    
}
