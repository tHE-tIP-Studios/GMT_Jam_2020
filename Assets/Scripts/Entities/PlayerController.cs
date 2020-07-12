using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0.1f;
    [SerializeField] private LayerMask _blockingLayer = default;
    [SerializeField] private Transform _feetPosition = default;

    private Animator _animator;
    private float _timeout;
    private LevelManager _level;
    private float _timeBeforeLastInput = 0;
    private bool _firstInput;
    private float _inverseMoveTime;
    private Rigidbody2D _rb;

    public bool InputTimeout
    {
        get => _timeout > 0;
        set
        {
            _timeout = .4f;
        }
    }
    public InputData NextInput { get; set; }
    public event Action<InputData> onNewInput;
    public UnityEvent onNewInputUnityEvent;
    public event Action onNewNextInput;
    /// <summary>
    /// Used to like, throw some particles from the player, like puff, when you interact
    /// </summary>
    public event Action onInteract;

    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("LevelManager");
        Debug.Log(obj.name);
        _level = obj.GetComponent<LevelManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void ResetPlayer()
    {
        _firstInput = true;
    }

    private void OnEnable()
    {
        _level.onLevelReset += ResetPlayer;
    }

    private void OnDisable()
    {
        _level.onLevelReset -= ResetPlayer;
    }

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inverseMoveTime = 1 / _moveTime;
        Debug.Log("Aaaaaaaa");
        _firstInput = true;
    }

    private void Update()
    {
        if (!_firstInput)
        {
            _timeBeforeLastInput += Time.deltaTime;
        }

        if (_level.PlayerTurn && _level.CurrentInputs < _level.MaxInputs)
        {
            // Get each input
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            // Process directions
            if (!InputTimeout)
            {

                if (Mathf.Abs(inputX) == 1f)
                {
                    Vector3 position = new Vector3(inputX, 0.0f, 0.0f);
                    InputTimeout = true;
                    AddNewInput(position);
                }
                else if (Mathf.Abs(inputY) == 1f)
                {
                    Vector3 position = new Vector3(0f, inputY, 0f);
                    // Add new input to the queue
                    InputTimeout = true;
                    AddNewInput(position);
                }
            }

            // Process buttons
        }

        if (_timeout > 0)
            _timeout -= Time.deltaTime;
    }

    public void Run()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        do
        {
            NextInput = _level.PlayerInputs.Dequeue();
            onNewNextInput?.Invoke();
            
            Debug.Log(NextInput.MoveAmount);
            
            // Wait for when the input should be called
            yield return new WaitForSeconds(NextInput.TimeBefore);

            // Process and call the input
            NextInput.onInput?.Invoke();

            if (NextInput.Type.Equals(InputType.Movement))
            {
                Move(NextInput.MoveAmount);
                // yield return new WaitForSeconds(_moveTime);
                _animator.SetBool("Walk", false);
                _animator.SetBool("Climb", false);
            }
        } while (_level.PlayerInputs.Count > 0);
    }

    private void Move(Vector3 toMove)
    {
        // Check if the move is possible
        // ...
        // Check if there is any ground beneath
        Collider2D col;
        col = Physics2D.OverlapCircle(_feetPosition.position, .4f, LayerMask.GetMask("Ground", "Climbables"));
        if (!col)
        {
            // add a -1 to the Y move component
            toMove = new Vector3(toMove.x, -2, 0.0f);
        }

        Vector3 end = transform.position + toMove;
        col = Physics2D.OverlapCircle(end, .3f, _blockingLayer);
        if (!col && toMove.y != 1)
        {
            _animator.SetBool("Walk", true);
            StartCoroutine(SmoothMovement(end));
        }
        else if (col)
        {
            IClimbable c = col.GetComponent<IClimbable>();
            if (c != null)
            {
                _animator.SetBool("Climb", true);
                StartCoroutine(SmoothMovement(end));
            }
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(_rb.position, end, _inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            _rb.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }

    private void InteractWith()
    {
        Collider2D col = Physics2D.OverlapCircle
                            (transform.position + transform.right,
                            0.4f, LayerMask.GetMask("Interactable"));
        if (col)
        {
            IInteractable interactable;
            if (col.TryGetComponent<IInteractable>(out interactable))
            {
                interactable.Interact();
                onInteract?.Invoke();
            }
        }
    }

    public void AddNewInput(Action onInput, KeyCode key)
    {
        if (!_level.PlayerTurn) return;
        if (_firstInput) _firstInput = false;

        // Sort for the key
        InputData data = new InputData
            (Vector3.zero, _timeBeforeLastInput, onInput, InputType.Action, key);

        _level.PlayerInputs.Enqueue(data);
        onNewInput?.Invoke(data);
        onNewInputUnityEvent?.Invoke();

        // Resets the time
        _timeBeforeLastInput = 0.0f;
    }

    public void AddNewInput(Vector3 moveAmount)
    {
        if (!_level.PlayerTurn) return;
        if (_firstInput) _firstInput = false;
        // Sort for the key
        KeyCode code = KeyCode.LeftArrow;

        if (moveAmount.x > 0)
            code = KeyCode.RightArrow;
        else if (moveAmount.x < 0)
            code = KeyCode.LeftArrow;
        else if (moveAmount.y > 0)
            code = KeyCode.UpArrow;
        else if (moveAmount.y < 0)
            code = KeyCode.DownArrow;

        InputData data = new InputData(moveAmount, _timeBeforeLastInput, InputType.Movement, code);
        _level.PlayerInputs.Enqueue(data);
        onNewInput?.Invoke(data);
        onNewInputUnityEvent?.Invoke();

        // Resets the time
        _timeBeforeLastInput = 0.0f;
    }

    public void AddNewInput(Vector3 moveAmount, Action onInput, InputType type)
    {
        if (!_level.PlayerTurn) return;
        if (_firstInput) _firstInput = false;
        KeyCode code = KeyCode.LeftArrow;

        if (moveAmount.x > 0)
            code = KeyCode.RightArrow;
        else if (moveAmount.x < 0)
            code = KeyCode.LeftArrow;
        else if (moveAmount.y > 0)
            code = KeyCode.UpArrow;
        else if (moveAmount.y < 0)
            code = KeyCode.DownArrow;

        InputData data = new InputData(moveAmount, _timeBeforeLastInput, onInput, type, code);
        _level.PlayerInputs.Enqueue(data);
        onNewInput?.Invoke(data);
        onNewInputUnityEvent?.Invoke();

        // Resets the time
        _timeBeforeLastInput = 0.0f;
    }
}