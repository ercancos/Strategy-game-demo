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

    #endregion

    public void TakeStructureToBuilt(GameObject structure)
    {
        _structure = structure;
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

    private void Update()
    {

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
        _structure = null;
    }

    private void DragStructure(Vector3 currentMousePos)
    {
        if (_structure != null)
        {
            _structure.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(currentMousePos);

            //Debug.Log("Structure dragged.");
        }
    }

    private void BuildStructure(Vector3 currentMousePos)
    {
        if (_structure != null)
        {
            _structure.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(currentMousePos);
            _structure.GetComponent<BoxCollider2D>().isTrigger = false;
            //Debug.Log("Built succesfully completed !");
            _structure = null;
            if (OnStructureBuildAction != null)
            {
                OnStructureBuildAction();
            }
        }
    }
}
