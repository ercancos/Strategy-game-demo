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
        createdObject.AddComponent<Barrack>();

        return createdObject;
    }
}
