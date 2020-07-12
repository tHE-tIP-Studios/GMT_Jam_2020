using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ExitLevel : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnExit = default;
    [SerializeField] private float _timeToWait = default;
    [SerializeField] private bool _isOpen = default;
    private WaitForSeconds _timer = default;
    private LevelManager _levelManager = default;
    private PlayerController _player = default;
    private BoxCollider2D _triggerZone = default;

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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_levelManager.CurrentInputs <= 0)
            {
                StartCoroutine(WaitBeforeLoadNextLevel());
            }
        }
    }
}
