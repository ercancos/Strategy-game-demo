using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script is responsible for control build operations of given structures.
 * 
 */

public class StructureBuildController : MonoBehaviour
{
    #region Variables

    public static StructureBuildController Instance;

    public delegate void OnStructureBuild();
    public static event OnStructureBuild OnStructureBuildAction;

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
        //Subscribe events.
        PlayerInteractionController.OnRightClickAction += CancelBuild;
        PlayerInteractionController.OnLeftClickAction += BuildStructure;
        PlayerInteractionController.OnMouseMoveAction += DragStructure;
    }

    private void OnDestroy()
    {
        //Unsubscribe events.
        PlayerInteractionController.OnRightClickAction -= CancelBuild;
        PlayerInteractionController.OnLeftClickAction -= BuildStructure;
        PlayerInteractionController.OnMouseMoveAction -= DragStructure;

    }

    //It cancels build operation.
    private void CancelBuild()
    {
        if (_structure != null)
        {
            Debug.Log("Built canceled !");
            PoolManager.Instance.RecycleObject(_structure);
            _structure = null;
        }
    }

    //It drags current assigned structure, if exist.
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

    //It builds current assigned structure, if exist.
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
            else
            {
                CreateWorldTextPopup("Can not build here !", Camera.main.ScreenToWorldPoint(currentMousePos), Color.white, (Vector2)Camera.main.ScreenToWorldPoint(currentMousePos) + new Vector2(0, 10));
            }
        }
    }

    //Checks the area is valid for construction.
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

    //Creates a Text Pop-up in the World.
    private void CreateWorldTextPopup(string text, Vector2 localPosition, Color color, Vector2 finalPopupPosition)
    {
        TextMesh popUpTextMesh = CreateWorldText(text, localPosition, color);
        //Vector2 moveAmount = (finalPopupPosition - localPosition) / 0.1f;
        //StartCoroutine(DestroyDelayedWorldText(popUpTextMesh.transform, moveAmount, 1f));
        StartCoroutine(DestroyDelayedWorldText(popUpTextMesh.transform, localPosition, finalPopupPosition, 1f));
    }

    //Creates a Text object in the World.
    private TextMesh CreateWorldText(string text, Vector3 localPosition, Color color)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;

        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = TextAnchor.LowerLeft;
        textMesh.alignment = TextAlignment.Left;
        textMesh.text = text;
        textMesh.fontSize = 10;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = 4;
        return textMesh;
    }

    //private IEnumerator DestroyDelayedWorldText(Transform popUpText, Vector3 moveAmount, float delayTime)
    private IEnumerator DestroyDelayedWorldText(Transform popUpText, Vector2 startPoint, Vector2 endPoint, float delayTime)
    {
        float distance = Vector2.Distance(startPoint, endPoint);

        // Calculate the number of intervals based on the distance and time
        int intervals = (int)(distance / delayTime);

        // Calculate the interval size
        float intervalSize = 1.0f / intervals;

        // Move the object to the end point in equal intervals
        for (float t = 0; t < 1.0f; t += intervalSize)
        {
            // Interpolate between the start and end points using Mathf.Lerp
            popUpText.position = Vector3.Lerp(startPoint, endPoint, t);

            // Wait for the next interval
            yield return new WaitForSeconds(intervalSize * delayTime);
        }

        Destroy(popUpText.gameObject);
    }
}
