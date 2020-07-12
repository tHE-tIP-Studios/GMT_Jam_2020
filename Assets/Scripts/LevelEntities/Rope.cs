using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, IClimbable
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveDuration;
    public int Length {get;}
    private LineHandler _line;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _current;
    private float _inverseMoveDuration;
    private void Awake() 
    {
        _endPosition = _startPosition + transform.right * _moveDistance;
        _inverseMoveDuration = 1/_moveDuration;
    }
    private void Start()
    {
        _line = GetComponentInChildren<LineHandler>();
        BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();

        collider2D.size = new Vector2(.8f, _line.LineLenght);
        collider2D.offset = new Vector2(0.0f, -_line.LineLenght/2);
    }

    public void NextPoint()
    {
        _current = _current == _startPosition ? _endPosition : _startPosition;
        StartCoroutine(MoveToPoint(_current));
    }

    private IEnumerator MoveToPoint(Vector3 point)
    {
        float remaningDist = (transform.position - point).sqrMagnitude;

        while(remaningDist > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, point, _inverseMoveDuration * Time.deltaTime);
            transform.position = newPos;

            remaningDist = (transform.position - point).sqrMagnitude;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 end = _startPosition + transform.right * _moveDistance;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_startPosition, end);
    }
}
