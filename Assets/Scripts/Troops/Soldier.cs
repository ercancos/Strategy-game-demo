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

    public override void Move(Transform targetObject)
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

    protected override void Start()
    {
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }
}
