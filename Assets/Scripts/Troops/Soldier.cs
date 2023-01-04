using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Soldier : Troop
{
    public override TroopType GetTroopType()
    {
        return TroopType.Military;
    }

    public override void MoveTo(Transform targetObject)
    {
        if (_aIDestinationSetter != null)
        {
            _aIDestinationSetter.target = targetObject;
        }
        else
        {
            Debug.LogError("_aIDestinationSetter is null");
        }
    }

    public override void MoveTo(Vector2 targetCoordinate)
    {
        if (_targetPositionGameObject != null)
        {
            //Set target position to target coordinate.
            _targetPositionGameObject.transform.position = targetCoordinate;

            SetTargetVisible(true);

            //Move to the target position.
            _aIDestinationSetter.target = _targetPositionGameObject.transform;
        }
    }

    //Sets target-position gameobject.
    public void SetTargetObject(GameObject obj)
    {
        _targetPositionGameObject = obj;
    }

    protected override void Start()
    {
        //Get AI destination-setter component.
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();

        //Get text object.
        _textObject = transform.GetChild(0).gameObject;
        _textObject.SetActive(false);

        //Get selected-outline object.
        _selectedOutlineGameObject = transform.GetChild(1).gameObject;

    }

    private void OnDestroy()
    {
        //Destroy target position gameObject.
        Destroy(_targetPositionGameObject);
    }
}
