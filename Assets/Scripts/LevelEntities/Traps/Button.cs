using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _OnInteract;

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

    public void Interact()
    {
        _OnInteract?.Invoke();
    }
}
