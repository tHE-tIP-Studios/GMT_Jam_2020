using UnityEngine;
using UnityEngine.EventSystems;

public class MenuMouseController : MonoBehaviour
{
    /// <summary>
    /// Variable to store last selected object in a menu
    /// </summary>
    private GameObject _lastselect;

    private void Start()
    {
        //* Get the current selected object when the scene loads
        _lastselect = EventSystem.current.gameObject;
    }

    private void Update()
    {
        //* If there's a mouse click...
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            //* ... in a place a button doesn't exit, don't let it be null...
            EventSystem.current.SetSelectedGameObject(_lastselect);
        }
        else
        {
            //* ... if there was a button, select it normally
            _lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

}
