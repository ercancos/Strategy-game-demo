using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : Factory
{
    public override GameObject CreateObject(Vector2 pos)
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject();
        //Set object name.
        createdObject.name = "Soldier";
        //Set object tag.
        createdObject.tag = "Troop";
        //Set object position to given position.
        createdObject.transform.position = pos;
        //Set object layer to obstacle layer(6).
        createdObject.layer = 6;

        //Add SpriteRenderer component to the object and set it's sprite.
        SpriteRenderer createdObjectSpriteRenderer = createdObject.AddComponent<SpriteRenderer>();
        createdObjectSpriteRenderer.sprite = objectData.GetSprite;

        //Add Rigidbody2D component to the object and set gravity to zero.
        Rigidbody2D createdObjectRigidbody2D = createdObject.AddComponent<Rigidbody2D>();
        createdObjectRigidbody2D.gravityScale = 0;

        //Add BoxCollider2D component to the object.
        createdObject.AddComponent<BoxCollider2D>();
        createdObject.transform.localScale = new Vector3(objectData.GetWidth, objectData.GetHeight, 1);

        //Add Barrack component to the object.
        createdObject.AddComponent<Soldier>();

        return createdObject;
    }
    protected override GameObject CreateGameObject()
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject();
        //Set object name.
        createdObject.name = "Soldier";
        //Set object tag.
        createdObject.tag = "Troop";
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
}
