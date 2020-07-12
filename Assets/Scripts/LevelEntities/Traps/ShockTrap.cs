using UnityEngine;

public class ShockTrap : Trap
{
    /// <summary>
    /// Method to be called when player enters the trap collider
    /// </summary>
    public override void TrapActivation(PlayerController player)
    {
        print(player.name);
        print(player.NextInput);
        InputData trapInput = new InputData(
            -player.NextInput.MoveAmount, player.NextInput.TimeBefore,
            player.NextInput.onInput, player.NextInput.Type);

        player.NextInput = trapInput;
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
            PlayerController p = other.GetComponent<PlayerController>();
            OnPlayerEnter?.Invoke();
            TrapActivation(p);
        }
    }
}
