//Libraries..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Troop : MonoBehaviour
{
    public enum TroopType
    {
        Military,
        FirstAid,
        Citizen
    }

    public abstract TroopType GetTroopType();

    public delegate void OnTroopSelected(GameObject obj);
    public static event OnTroopSelected OnTroopSelectedAction;

    protected GameObject _textObject;
    protected AIDestinationSetter _aIDestinationSetter;

    public abstract void Move(Transform targetObject);

    protected abstract void Start();

    protected virtual void OnMouseEnter()
    {
        if (_textObject != null)
        {
            _textObject.SetActive(true);
        }
    }

    protected virtual void OnMouseExit()
    {
        if (_textObject != null)
        {
            _textObject.SetActive(false);
        }
    }

    protected virtual void OnMouseDown()
    {
        if (OnTroopSelectedAction != null)
        {
            OnTroopSelectedAction(this.gameObject);
        }
    }
}
