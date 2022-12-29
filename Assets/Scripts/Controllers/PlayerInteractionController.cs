//Libraries..
using UnityEngine;

/*
 * 
 * This script is responsible for control player interactions and fire necessary events.
 * 
 */

public class PlayerInteractionController : MonoBehaviour
{
    #region Variables

    public static PlayerInteractionController Instance;

    public delegate void OnClickScroll();
    public static event OnClickScroll OnClickScrollAction;

    public delegate void OnMouseMove(Vector3 currentPos);
    public static event OnMouseMove OnMouseMoveAction;

    public delegate void OnMouseScrollMove(Vector2 currentPos);
    public static event OnMouseScrollMove OnMouseScrollMoveAction;

    private Vector2 _previousMousePosition;

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Set previous mouse position on start.
        _previousMousePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse has moved.
        if (CheckIsMouseMove())
        {
            OnMouseMoveAction(Input.mousePosition);
            //Debug.Log("mouse has moved !");
        }

        // Check if the mouse-scroll has moved.
        if (CheckIsScrollMove())
        {
            OnMouseScrollMoveAction(Input.mouseScrollDelta);
            //Debug.Log("mouse-scroll has moved !");
        }

        // Check if middle mouse button(scroll) pressed.
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (OnClickScrollAction != null)
            {
                OnClickScrollAction();
            }
        }
    }

    // This function checks whether mouse is move or not.
    private bool CheckIsMouseMove()
    {
        Vector2 currentMousePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Check if the mouse has moved from its previous position.
        if (currentMousePosition != _previousMousePosition)
        {
            // Update the previous mouse position
            _previousMousePosition = currentMousePosition;
            return true;
        }

        return false;
    }

    // This function checks whether mouse scroll is move or not.
    private bool CheckIsScrollMove()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            return true;
        }

        return false;
    }
}
