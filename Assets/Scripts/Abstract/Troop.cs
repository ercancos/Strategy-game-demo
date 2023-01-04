//Libraries..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Troop : MonoBehaviour, IDamageable
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
    protected GameObject _selectedOutlineGameObject;
    protected GameObject _targetPositionGameObject;
    protected AIDestinationSetter _aIDestinationSetter;

    public abstract void MoveTo(Transform targetObject);
    public abstract void MoveTo(Vector2 targetCoordinate);

    public virtual void SetSelectedVisible(bool visible)
    {
        _selectedOutlineGameObject.SetActive(visible);
    }

    public virtual void SetTargetVisible(bool visible)
    {
        _targetPositionGameObject.SetActive(visible);
    }

    protected virtual void Start()
    {
        _textObject = transform.GetChild(0).gameObject;
        _textObject.SetActive(false);
    }

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


    //
    //Future updates functions.
    public void Damage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public bool IsObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
    //
    //
}
