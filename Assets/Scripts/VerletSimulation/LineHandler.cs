using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineHandler : MonoBehaviour
{
    [SerializeField] private float SECTION_LENGTH = 1f;
    [SerializeField] private float _dampeningAmount = .4f;
    [SerializeField] private int _resolution;
    [SerializeField] private Transform _endpoint;
    private LineRenderer _line;
    private Transform _floaterPos;
    private VerletLinePoint[] _points;
    private float _dampening;
    private bool _hang;

    public Transform EndPoint { get; set; }

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _dampening = _dampeningAmount;
        _line.positionCount = _resolution;
        if (_endpoint != null)
            NewTarget(_endpoint);
        InitRope();
    }

    private void InitRope()
    {
        Vector3 pointPosition = transform.position;

        _points = new VerletLinePoint[_resolution];
        for (int i = 0; i < _resolution; i++)
        {
            _points[i] = new VerletLinePoint(pointPosition, Physics.gravity * 20);

            pointPosition.y -= SECTION_LENGTH;
        }
    }

    private void Update()
    {
        if (_line != null)
            _line.SetPosition(0, transform.position);
        
        DisplayRope();
    }

    private void FixedUpdate()
    {
        UpdateRopeSimulation(Time.fixedDeltaTime);
    }

    private void DisplayRope()
    {
        for (int i = 0; i < _resolution; i++)
        {
            _line.SetPosition(i, _points[i].Pos);
        }
        if (_hang)
        {
            Vector3 dir = _points[_resolution - 2].Pos - _floaterPos.position;
            _floaterPos.up = dir;
        }
    }

    private void UpdateRopeSimulation(float dt)
    {
        // FIrst rope section to the tip of the rod
        _points[0].Pos = transform.position;
        if (_hang)
            EndPoint.position = _points[_resolution - 1].Pos;
        else
            _points[_resolution - 1].Pos = EndPoint.position;
        UpdateVerlet(dt);

        // Check for the currect section lenght
        // The more iterations you do here the more realistic it will look
        for (int i = 0; i < 20; i++)
        {
            UpdateConstrains();
        }
    }

    private void UpdateConstrains()
    {
        for (int i = 0; i < _resolution - 1; i++)
        {
            ConstrainLine(ref _points[i], ref _points[i + 1], SECTION_LENGTH);
        }
    }

    private void ConstrainLine(ref VerletLinePoint p1, ref VerletLinePoint p2,
        float restLength)
    {
        Vector3 delta = p2.Pos - p1.Pos;
        float deltaLength = delta.magnitude;

        float diff = (deltaLength - restLength) / deltaLength;

        p1.Pos += delta * diff * SECTION_LENGTH;
        p2.Pos -= delta * diff * SECTION_LENGTH;
    }

    private void UpdateVerlet(float dt)
    {
        for (int i = 0; i < _resolution; i++)
        {
            Verlet(ref _points[i], dt);
        }
    }

    /// <summary>
    /// Calculates the verlet acceleration using a point and a time delta
    /// </summary>
    /// <param name="point"> point to calculate for</param>
    /// <param name="dt"> delta time</param>
    private void Verlet(ref VerletLinePoint point, float dt)
    {
        Vector3 temp = point.Pos;
        point.Pos += ((point.Pos - point.OldPos) * _dampening) + (point.Acceleration * dt * dt);
        point.OldPos = temp;
    }

    public void NewTarget(Transform target)
    {
        _floaterPos = target;
        EndPoint = target;
    }

    public IEnumerator LerpDampening(float dampValue)
    {
        _dampening = 0;
        while (_dampening < dampValue)
        {
            yield return null;
            _dampening += Time.deltaTime;
        }
    }
}