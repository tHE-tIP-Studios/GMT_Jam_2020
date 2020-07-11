using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Trap : MonoBehaviour
{
    protected BoxCollider2D _triggerZone = default;

    protected virtual void Awake()
    {
        _triggerZone = GetComponent<BoxCollider2D>();
        _triggerZone.isTrigger = true;
        _triggerZone.size = new Vector2(1f, 1.2f);
        _triggerZone.offset = new Vector2(0f, 0.1f);
    }

    public abstract void TrapEvent(PlayerController player);
    public UnityEvent OnPlayerEnter;
}
