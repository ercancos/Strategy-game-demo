using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform _selectionAreaTransform;

    private Vector2 _startPosition;
    private List<Troop> _selectedUnitList;

    #endregion

    private void Awake()
    {
        _selectedUnitList = new List<Troop>();
        _selectionAreaTransform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerInteractionController.OnLeftClickAction += SetStartPosition;
        PlayerInteractionController.OnLeftClickReleaseAction += SelectSoldiersInArea;
        PlayerInteractionController.OnLeftClickHoldPressedAction += SetSelectionArea;
        PlayerInteractionController.OnRightClickAction += MoveSelectedSoldiers;
    }

    private void OnDestroy()
    {
        PlayerInteractionController.OnLeftClickAction -= SetStartPosition;
        PlayerInteractionController.OnLeftClickReleaseAction -= SelectSoldiersInArea;
        PlayerInteractionController.OnLeftClickHoldPressedAction -= SetSelectionArea;
        PlayerInteractionController.OnRightClickAction -= MoveSelectedSoldiers;
    }

    //This function sets start position.
    private void SetStartPosition(Vector3 currentMousePos)
    {
        _startPosition = Camera.main.ScreenToWorldPoint(currentMousePos);
        _selectionAreaTransform.gameObject.SetActive(true);
    }

    //This function sets the selection area and visualize it for player.
    private void SetSelectionArea(Vector3 currentMousePos)
    {
        Vector2 currentMousePosInworld = Camera.main.ScreenToWorldPoint(currentMousePos);

        Vector2 lowerLeft = new Vector2(
            Mathf.Min(_startPosition.x, currentMousePosInworld.x),
            Mathf.Min(_startPosition.y, currentMousePosInworld.y));

        Vector2 upperRight = new Vector2(
            Mathf.Max(_startPosition.x, currentMousePosInworld.x),
            Mathf.Max(_startPosition.y, currentMousePosInworld.y));

        _selectionAreaTransform.position = lowerLeft;
        _selectionAreaTransform.localScale = upperRight - lowerLeft;
    }

    //This function fill the list by detecting troop objects in the given area.
    private void SelectSoldiersInArea(Vector3 currentMousePos)
    {
        _selectionAreaTransform.gameObject.SetActive(false);

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPosition, Camera.main.ScreenToWorldPoint(currentMousePos));

        foreach (Troop troop in _selectedUnitList)
        {
            troop.SetSelectedVisible(false);
        }

        _selectedUnitList.Clear();

        foreach (Collider2D collider2D in collider2DArray)
        {
            Troop troop = collider2D.GetComponent<Troop>();

            if (troop != null)
            {
                troop.SetSelectedVisible(true);
                _selectedUnitList.Add(troop);
            }
        }
    }

    // This function moves selected soldiers to clicked location.
    private void MoveSelectedSoldiers()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(PlayerInteractionController.GetMousePosition());

        foreach (Troop troop in _selectedUnitList)
        {
            troop.MoveTo(currentMousePosition);
        }
    }
}
