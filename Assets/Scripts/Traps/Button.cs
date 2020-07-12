using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Trap
{
    private PlayerController _player = default;

    /// <summary>
    /// Override method to make use of the events to open doors or move objects
    /// </summary>
    /// <param name="player"> Reference to the player script </param>
    public override void TrapActivation(PlayerController player)
    {
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
            TryGetComponent<PlayerController>(out _player);
        }
    }
}
