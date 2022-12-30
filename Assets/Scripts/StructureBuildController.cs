using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBuildController : MonoBehaviour
{
    #region Variables

    public static StructureBuildController Instance;

    public delegate void OnStructureBuild();
    public static event OnStructureBuild OnStructureBuildAction;

    private bool _isStructureBuild = false;
    private GameObject _structure;
    private SpriteRenderer _structureSpriteRenderer;
    private Structure _structureBaseClass;

    #endregion

    public void TakeStructureToBuilt(GameObject structure)
    {
        _structure = structure;
        _structureSpriteRenderer = _structure.GetComponent<SpriteRenderer>();
        _structureBaseClass = _structure.GetComponent<Structure>();
    }

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

    private void OnEnable()
    {
        PlayerInteractionController.OnRightClickAction += CancelBuild;
        PlayerInteractionController.OnLeftClickAction += BuildStructure;
        PlayerInteractionController.OnMouseMoveAction += DragStructure;
    }

    private void OnDestroy()
    {
        PlayerInteractionController.OnRightClickAction -= CancelBuild;
        PlayerInteractionController.OnLeftClickAction -= BuildStructure;
        PlayerInteractionController.OnMouseMoveAction -= DragStructure;

    }

    private void CancelBuild()
    {
        Debug.Log("Built canceled !");
        PoolManager.Instance.RecycleObject(_structure);
        _structure = null;
    }

    private void DragStructure(Vector3 currentMousePos)
    {
        if (_structure != null)
        {
            _structure.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(currentMousePos);
            if (IsValidConstructionArea())
            {
                if (_structureSpriteRenderer != null)
                {
                    _structureSpriteRenderer.color = Color.green;
                }
            }
            else
            {
                if (_structureSpriteRenderer != null)
                {
                    _structureSpriteRenderer.color = Color.red;
                }
            }
        }
    }

    private void BuildStructure(Vector3 currentMousePos)
    {
        if (_structure != null)
        {
            if (IsValidConstructionArea())
            {
                _structure.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(currentMousePos);
                _structure.GetComponent<BoxCollider2D>().isTrigger = false;
                if (_structureSpriteRenderer != null)
                {
                    _structureSpriteRenderer.color = Color.white;
                }

                _structure = null;
                if (OnStructureBuildAction != null)
                {
                    OnStructureBuildAction();
                }
            }
        }
    }

    private bool IsValidConstructionArea()
    {
        if (_structureBaseClass != null)
        {
            if (_structureBaseClass.IsOverlapToAnotherObject)
            {
                return false;
            }
            return true;
        }

        return false;
    }
}
