//Libraries..
using UnityEngine;

/*
 * 
 * 
 * 
 */

public abstract class Factory : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Tooltip("Data object that includes all of sprites, object name, dimensions and so on.")]
    protected PlacedObjectDataSO objectData;

    #endregion

    public PlacedObjectDataSO ObjectData { get => objectData; }

    //Create a structure depending on the concrete-class factory implementation.
    public abstract GameObject CreateObject(Vector2 pos);

    //Helper functions.
    //
    //Creates main object.
    protected abstract GameObject CreateGameObject();
    //Adds sprite component to the created object.
    protected abstract void AddSpriteComponent(GameObject obj);
    //Adds Rigidbody2D component to the created object.
    protected abstract void AddRigidbody2DComponent(GameObject obj);
    //Adds Collider component to the created object.
    protected abstract void AddColliderComponent(GameObject obj);
    //Sets scale of the created object.
    protected abstract void SetObjectScale(GameObject obj);
    //Creates text object and set as child of the created object.
    protected abstract void CreateTextObject(Vector2 pos, GameObject parentObject);
}
