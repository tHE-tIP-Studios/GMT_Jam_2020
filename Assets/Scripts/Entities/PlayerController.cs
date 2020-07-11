using System.Collections;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0.1f;
    [SerializeField] private LayerMask _blockingLayer = default;

    private Animator _animator;
    private LevelManager _level;
    private float _timeBeforeLastInput = 0;
    private bool _firstInput;
    private float _inverseMoveTime;
    private Rigidbody2D _rb;

    public InputData NextInput{get; set;}
    public event Action<InputData> onNewInput;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inverseMoveTime = 1/_moveTime;
        Debug.Log("Aaaaaaaa");
        GameObject obj = GameObject.FindGameObjectWithTag("LevelManager");
        Debug.Log(obj.name);
        _level = obj.GetComponent<LevelManager>();
        _firstInput = true;
    }

    private void Update() 
    {
        if (!_firstInput)
        {
            _timeBeforeLastInput += Time.deltaTime;
        }

        if (_level.PlayerTurn && _level.InputsAmount > 0)
        {
            // Get each input
            float inputX = Input.GetAxisRaw("Horizontal");         
            float inputY = Input.GetAxisRaw("Vertical");         
            // Process directions
            if (Mathf.Abs(inputX) == 1f)
            {
                Vector3 position = new Vector3(inputX, 0.0f, 0.0f);
                Collider2D hit = Physics2D.OverlapCircle
                    (transform.position + position, .2f, _blockingLayer);
                if (!hit)
                {
                    AddNewInput(position, InputType.Movement);
                }
            }
            else if (Mathf.Abs(inputY) == 1f)
            {
                Vector3 position = new Vector3(0f, inputY, 0f);
                Collider2D hit = Physics2D.OverlapCircle
                    (transform.position + position, .2f, _blockingLayer);
                if (!hit && inputY != 1)
                {
                    // Add new input to the queue
                    AddNewInput(position, InputType.Movement);
                }
            }

            // Process buttons
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Run();
        }
    }

    public void Run()
    {
        if (_level.CurrentInputs < _level.MaxInputs) return;
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        NextInput = _level.PlayerInputs.Dequeue();
        do
        {
            Debug.Log(NextInput.MoveAmount);
            // Wait for when the input should be called
            yield return new WaitForSeconds(NextInput.TimeBefore);

            // Process and call the input
            if (NextInput.onInput != null)
            {
                NextInput.onInput.Invoke();
            }
            if (NextInput.Type.Equals(InputType.Movement))
            {
                Move(NextInput.MoveAmount);
            }

            NextInput = _level.PlayerInputs.Dequeue();

        }while(_level.PlayerInputs.Count > 0);
    }

    private void Move(Vector3 toMove)
    {
        Vector3 end = transform.position + toMove;
        StartCoroutine(SmoothMovement(end));
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while(sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(_rb.position, end, _inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            _rb.MovePosition (newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }

    public void AddNewInput(Vector3 moveAmount, InputType type)
    {
        if (!_level.PlayerTurn) return;
        if (_firstInput) _firstInput = false;
        InputData data = new InputData(moveAmount, _timeBeforeLastInput, type);
        _level.PlayerInputs.Enqueue(data);
        onNewInput?.Invoke(data);
        
        // Resets the time
        _timeBeforeLastInput = 0.0f;
    }

    public void AddNewInput(Vector3 moveAmount, Action onInput, InputType type)
    {
        if (!_level.PlayerTurn) return;
        if (_firstInput) _firstInput = false;
        InputData data = new InputData(moveAmount, _timeBeforeLastInput, onInput, type);
        _level.PlayerInputs.Enqueue(data);
        onNewInput?.Invoke(data);
        
        // Resets the time
        _timeBeforeLastInput = 0.0f;
    }
}