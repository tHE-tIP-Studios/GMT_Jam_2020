using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : Trap
{
    [SerializeField] private float _timeToWait = default;
    private WaitForSeconds _timer = default;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _timer = new WaitForSeconds(_timeToWait);
    }

    public override void TrapActivation(PlayerController player)
    {
        StartCoroutine(WaitBeforeLoadNextLevel());
    }

    private IEnumerator WaitBeforeLoadNextLevel()
    {
        //TODO Have a level cleared animation
        yield return(_timer);
        OnPlayerEnter?.Invoke();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.TryGetComponent<PlayerController>(out PlayerController p);
            TrapActivation(p);
        }
    }
}
