using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public enum StructureType
    {
        Military,
        Energy
    }

    public delegate void OnStructureSelected(GameObject obj);
    public static event OnStructureSelected OnStructureSelectedAction;

    protected bool _isOverlapToAnotherObject;
    protected GameObject _textObject;

    public bool IsOverlapToAnotherObject { get => _isOverlapToAnotherObject; }

    public abstract StructureType GetStructureType();

    protected virtual void Start()
    {
        _textObject = transform.GetChild(0).gameObject;
        _textObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        _isOverlapToAnotherObject = true;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        _isOverlapToAnotherObject = true;
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        _isOverlapToAnotherObject = false;
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
        if (OnStructureSelectedAction != null)
        {
            OnStructureSelectedAction(this.gameObject);
        }
    }
}
