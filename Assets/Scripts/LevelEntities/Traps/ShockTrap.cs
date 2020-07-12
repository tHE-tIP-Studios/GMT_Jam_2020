using UnityEngine;

public class ShockTrap : Trap
{
    /// <summary>
    /// Method to be called when player enters the trap collider
    /// </summary>
    public override void TrapActivation(PlayerController player)
    {
        throw new System.NotImplementedException();
        //TODO Access NextInput property, and change Key value to the inverse of the selected
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
            PlayerController p = GetComponent<PlayerController>();
            OnPlayerEnter?.Invoke();
            //TODO Add TrapEvent() to NextInput onInput action
        }
    }
}
