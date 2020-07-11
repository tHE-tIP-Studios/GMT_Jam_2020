using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Trap : MonoBehaviour
{
    /// <summary>
    /// Box collider variable to change values on Awake(), to be used by all
    /// traps that extend this class
    /// </summary>
    protected BoxCollider2D _triggerZone = default;

    protected virtual void Awake()
    {
        _triggerZone = GetComponent<BoxCollider2D>();
        _triggerZone.isTrigger = true;
        _triggerZone.size = new Vector2(1f, 1.2f);
        _triggerZone.offset = new Vector2(0f, 0.1f);
    }

    /// <summary>
    /// Method that does the trap action
    /// </summary>
    /// <param name="player">
    /// Reference to the player script to change 
    /// NextInput value or do something else
    /// </param>
    public abstract void TrapActivation(PlayerController player);

    /// <summary>
    /// Unity Event to add visual effects or other things
    /// </summary>
    public UnityEvent OnPlayerEnter;
}
