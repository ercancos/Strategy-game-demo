//Libraries..
using UnityEngine;

/*
 * 
 * 
 * 
 */

public abstract class Factory : MonoBehaviour
{
    [SerializeField]
    protected PlacedObjectDataSO objectData;

    //Create a structure depending on the concrete-class factory implementation.
    public abstract GameObject CreateObject(Vector2 pos);

    //Helper functions.
    protected abstract GameObject CreateGameObject();
    protected abstract void AddSpriteComponent(GameObject obj);
    protected abstract void AddRigidbody2DComponent(GameObject obj);
    protected abstract void AddColliderComponent(GameObject obj);
    protected abstract void SetObjectScale(GameObject obj);
    protected abstract void AddTextObject(Vector2 pos, GameObject parentObject);
}
