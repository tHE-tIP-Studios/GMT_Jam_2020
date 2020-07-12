using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ExitLevel : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnExit = default;
    [SerializeField] private float _timeToWait = default;
    [SerializeField] private bool _isOpen = default;
    [SerializeField] private GameObject[] _sprites;
    private WaitForSeconds _timer = default;
    private LevelManager _levelManager = default;
    private PlayerController _player = default;
    private BoxCollider2D _triggerZone = default;
    private bool _wasUsed = default;

    private void Awake()
    {
        _timer = new WaitForSeconds(_timeToWait);

        _levelManager = GameObject.FindGameObjectWithTag(
            "LevelManager").GetComponent<LevelManager>();

        _triggerZone = GetComponent<BoxCollider2D>();
        _triggerZone.isTrigger = true;
        _triggerZone.size = new Vector2(1f, 1.2f);
        _triggerZone.offset = new Vector2(0f, 0.1f);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (_isOpen)
        {
            _sprites[0].SetActive(false);
            _sprites[1].SetActive(true);
        }
    }

    private IEnumerator WaitBeforeLoadNextLevel()
    {
        //TODO Have a level cleared animation
        yield return (_timer);
        _OnExit?.Invoke();
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_levelManager.CurrentInputs <= 0 && _isOpen && !_wasUsed)
            {
                _wasUsed = true;
                StartCoroutine(WaitBeforeLoadNextLevel());
            }
        }
    }

    public void OpenExit()
    {
        if (_isOpen == false)
        {
            _isOpen = !_isOpen;
            _sprites[0].SetActive(false);
            _sprites[1].SetActive(true);
        }
    }
}
