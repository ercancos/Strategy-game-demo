//Libraries..
using UnityEngine;

/*
 * 
 * This script is responsible for camera action controls.
 * 
 */

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Tooltip("Camera movement speed value.")]
    private float cameraMovementSpeed = 20f;

    [SerializeField]
    [Tooltip("Camera movement border values.")]
    private Vector2 borderValues = new Vector2(30, 40);

    [SerializeField]
    [Tooltip("Camera movement area modifier value on screen edges.")]
    private float movementAreaModifier = 10f;

    [SerializeField]
    [Tooltip("Maximum zoom-in value.")]
    private int maxZoomInValue = 8;

    [SerializeField]
    [Tooltip("Maximum zoom-out value.")]
    private int maxZoomOutValue = 24;

    private float _defaultCameraSize;
    private Vector2 _previousMousePosition;

    #endregion

    private void OnEnable()
    {
        //Subscribe events.
        PlayerInteractionController.OnMouseMoveAction += UpdateLatestMousePosition;
        PlayerInteractionController.OnClickScrollAction += ResetCameraSize;
        PlayerInteractionController.OnMouseScrollMoveAction += ZoomCamera;
    }

    private void Start()
    {
        //Gets default size of camera.
        _defaultCameraSize = Camera.main.orthographicSize; ;
    }

    private void Update()
    {
        //Moves camera.
        MoveCamera();
    }

    private void OnDestroy()
    {
        //Unsubscribe events.
        PlayerInteractionController.OnMouseMoveAction -= UpdateLatestMousePosition;
        PlayerInteractionController.OnClickScrollAction -= ResetCameraSize;
        PlayerInteractionController.OnMouseScrollMoveAction -= ZoomCamera;
    }

    //This function resets camera size to default.
    private void ResetCameraSize()
    {
        Camera.main.orthographicSize = _defaultCameraSize;
    }

    //This function zooms camera according to player interaction.
    private void ZoomCamera(Vector2 currentMouseScrollPos)
    {
        //Check the value and perform zoom-in.
        if (currentMouseScrollPos.y > 0)
        {
            Camera.main.orthographicSize--;
            if (Camera.main.orthographicSize < maxZoomInValue)
            {
                Camera.main.orthographicSize = maxZoomInValue;
            }
        }
        //Check the value and perform zoom-out.
        else if (currentMouseScrollPos.y < 0)
        {
            Camera.main.orthographicSize++;
            if (Camera.main.orthographicSize > maxZoomOutValue)
            {
                Camera.main.orthographicSize = maxZoomOutValue;
            }
        }
    }

    //It updates latest mouse position when mouse moved.
    private void UpdateLatestMousePosition(Vector3 currentMousePos)
    {
        _previousMousePosition = currentMousePos;
    }

    //This function moves camera according to player interaction.
    private void MoveCamera()
    {
        //If y axis of mouse position is bigger than the movementAreaModifier value than camera will be move accordingly. 
        if (_previousMousePosition.y >= Screen.height - movementAreaModifier)
        {
            MoveCameraYAxis(Vector2Int.up);
        }

        //If y axis of mouse position is less than the movementAreaModifier value than camera will be move accordingly. 
        if (_previousMousePosition.y <= movementAreaModifier)
        {
            MoveCameraYAxis(Vector2Int.down);
        }

        //If x axis of mouse position is bigger than the movementAreaModifier value than camera will be move accordingly. 
        if (_previousMousePosition.x >= Screen.width - movementAreaModifier)
        {
            MoveCameraXAxis(Vector2Int.right);
        }

        //If x axis of mouse position is less than the movementAreaModifier value than camera will be move accordingly. 
        if (_previousMousePosition.x <= movementAreaModifier)
        {
            MoveCameraXAxis(Vector2Int.left);
        }
    }

    //This function moves camera on Y-axis.
    private void MoveCameraYAxis(Vector2Int direction)
    {
        Vector3 targetPosition = transform.position;

        //Move the camera if it is in the borders.
        if ((direction == Vector2Int.up) && !(transform.position.y > borderValues.y))
        {
            targetPosition.y += cameraMovementSpeed * Time.deltaTime;
        }
        else if ((direction == Vector2Int.down) && !(transform.position.y < -(borderValues.y)))
        {
            targetPosition.y -= cameraMovementSpeed * Time.deltaTime;
        }

        transform.position = targetPosition;
    }

    //This function moves camera on X-axis.
    private void MoveCameraXAxis(Vector2Int direction)
    {
        Vector3 targetPosition = transform.position;

        //Move the camera if it is in the borders.
        if ((direction == Vector2Int.right) && !(transform.position.x > borderValues.x))
        {
            targetPosition.x += cameraMovementSpeed * Time.deltaTime;
        }
        else if ((direction == Vector2Int.left) && !(transform.position.x < -(borderValues.x)))
        {
            targetPosition.x -= cameraMovementSpeed * Time.deltaTime;
        }

        transform.position = targetPosition;
    }
}
