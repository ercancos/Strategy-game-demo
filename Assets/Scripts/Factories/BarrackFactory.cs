//Libraries..
using UnityEngine;

public class BarrackFactory : Factory
{
    public override GameObject CreateObject(Vector2 pos)
    {
        //Create a new gameobject.
        GameObject createdObject = CreateGameObject();

        //Add sprite-renderer component.
        AddSpriteComponent(createdObject);

        //Add rigidbody2d component.
        AddRigidbody2DComponent(createdObject);

        //Add collider component.
        AddColliderComponent(createdObject);

        //Set object scale.
        SetObjectScale(createdObject);

        //Get collider upper bound.
        Bounds bounds = createdObject.GetComponent<BoxCollider2D>().bounds;

        //Add text object.
        AddTextObject(new Vector2((bounds.min.x - bounds.extents.x * 4.2f), (bounds.max.y * 8.8f)), createdObject);

        //Add spawn point.
        CreateSpawnPointObject(createdObject);

        //Add Barrack component to the object.
        createdObject.AddComponent<Barrack>();

        return createdObject;
    }

    protected override GameObject CreateGameObject()
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject();
        //Set object name.
        createdObject.name = "Barrack";
        //Set object tag.
        createdObject.tag = "Structure";
        //Set object position to given position.
        //createdObject.transform.position = pos;
        //Set object layer to obstacle layer(6).
        createdObject.layer = 6;

        return createdObject;
    }

    protected override void AddSpriteComponent(GameObject obj)
    {
        //Add SpriteRenderer component to the object and set it's sprite.
        SpriteRenderer createdObjectSpriteRenderer = obj.AddComponent<SpriteRenderer>();
        createdObjectSpriteRenderer.sprite = objectData.GetSprite;
    }

    protected override void AddRigidbody2DComponent(GameObject obj)
    {
        //Add Rigidbody2D component to the object
        Rigidbody2D createdObjectRigidbody2D = obj.AddComponent<Rigidbody2D>();
        //Set gravity to zero.
        createdObjectRigidbody2D.gravityScale = 0;
    }

    protected override void AddColliderComponent(GameObject obj)
    {
        //Add BoxCollider2D component to the object.
        BoxCollider2D boxCollider2D = obj.AddComponent<BoxCollider2D>();
        //Set object trigger mode true.
        boxCollider2D.isTrigger = true;
        //Set collider size to 0.8f both axis.
        boxCollider2D.size = new Vector2(0.8f, 0.8f);
    }

    protected override void SetObjectScale(GameObject obj)
    {
        obj.transform.localScale = new Vector3(objectData.GetWidth, objectData.GetHeight, 1);
    }

    protected override void AddTextObject(Vector2 pos, GameObject parentObject)
    {
        //Add text component.
        GameObject textObject = new GameObject("Text");
        TextMesh textObjectTextMesh = textObject.AddComponent<TextMesh>();
        textObjectTextMesh.text = objectData.GetName;

        //Set object position to top of parent object.
        textObject.transform.parent = parentObject.transform;
        textObject.transform.position = pos;
    }

    private void CreateSpawnPointObject(GameObject obj)
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject("SpawnPoint");
        createdObject.transform.parent = obj.transform;

        createdObject.transform.position = new Vector3(
        obj.transform.position.x,
        obj.transform.position.y - 3f,
        obj.transform.position.z);

        SpriteRenderer spriteRenderer = createdObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ((BarrackSO)objectData).SpawnPointIndicator;
    }
}
