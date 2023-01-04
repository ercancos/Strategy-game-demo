//Libraries..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script is responsible for control of troops.
 * 
 */

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
        if (_selectionAreaTransform != null)
        {
            _selectionAreaTransform.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        //Subscribe events.
        PlayerInteractionController.OnLeftClickAction += SetStartPosition;
        PlayerInteractionController.OnLeftClickReleaseAction += SelectSoldiersInArea;
        PlayerInteractionController.OnLeftClickHoldPressedAction += SetSelectionArea;
        PlayerInteractionController.OnRightClickAction += MoveSelectedSoldiers;
    }

    private void OnDestroy()
    {
        //Unsubscribe events.
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
        //Take mouse position.
        Vector2 currentMousePosInworld = Camera.main.ScreenToWorldPoint(currentMousePos);

        //Calculate lower left coordinate.
        Vector2 lowerLeft = new Vector2(
            Mathf.Min(_startPosition.x, currentMousePosInworld.x),
            Mathf.Min(_startPosition.y, currentMousePosInworld.y));

        //Calculate upper right coordinate.
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

        //Create an area to detect objects.
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPosition, Camera.main.ScreenToWorldPoint(currentMousePos));

        foreach (Troop troop in _selectedUnitList)
        {
            //Set inactive selected-visibles of previous selected troops.
            troop.SetSelectedVisible(false);

            //Set inactive target-visibles of previous selected troops.
            troop.SetTargetVisible(false);
        }

        //Remove all previous troops from list.
        _selectedUnitList.Clear();

        //Add detected troops to list from the selected area.
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

        StartCoroutine(DelayedMove(currentMousePosition, 0.05f));
    }

    private IEnumerator DelayedMove(Vector2 currentMousePosition, float delay)
    {
        foreach (Troop troop in _selectedUnitList)
        {
            troop.MoveTo(currentMousePosition);
            yield return new WaitForSeconds(delay);
        }
    }
}
