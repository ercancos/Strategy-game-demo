//Libraries..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SoldierFactory : Factory
{
    //Creates soldier object.
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

        //Add PathFinding component.
        AddPathFindingComponents(createdObject);

        //Add text object.
        CreateTextObject(new Vector2((bounds.min.x - bounds.extents.x * 2.2f), (bounds.max.y * 2.4f)), createdObject);

        //Add selected-outline object.
        CreateSelectedOutlineObject(createdObject);

        //Add target object.
        GameObject targetGameObject = CreateTargetPositionObject();

        //Add Soldier component to the object.
        Soldier soldier = createdObject.AddComponent<Soldier>();
        soldier.SetTargetObject(targetGameObject);

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
        //createdObjectSpriteRenderer.size = createdObjectSpriteRenderer.size * 28;
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

    protected override void CreateTextObject(Vector2 pos, GameObject parentObject)
    {
        //Add text component.
        GameObject textObject = new GameObject("Text");
        TextMesh textObjectTextMesh = textObject.AddComponent<TextMesh>();
        textObjectTextMesh.text = objectData.GetName;
        textObjectTextMesh.fontSize = 8;

        //Set object position to top of parent object.
        textObject.transform.parent = parentObject.transform;
        textObject.transform.position = pos;
    }

    //Creates selected-outline gameObject.
    private void CreateSelectedOutlineObject(GameObject obj)
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject("SelectedOutline");
        createdObject.transform.parent = obj.transform;

        SpriteRenderer spriteRenderer = createdObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ((SoldierSO)objectData).SelectedOutline;
        spriteRenderer.color = Color.green;

        createdObject.SetActive(false);
    }

    //Creates target-position gameObject.
    private GameObject CreateTargetPositionObject()
    {
        //Create a new gameobject.
        GameObject createdObject = new GameObject("TargetPosition");

        SpriteRenderer spriteRenderer = createdObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ((SoldierSO)objectData).TargetPosition;
        spriteRenderer.color = Color.red;

        createdObject.SetActive(false);

        return createdObject;
    }

    private void AddPathFindingComponents(GameObject obj)
    {
        //Add seeker component.
        obj.AddComponent<Seeker>();

        //Add AIPath component.
        AIPath aiPath = obj.AddComponent<AIPath>();
        //For 2d orientation.
        aiPath.orientation = OrientationMode.YAxisForward;

        //Add AIDestinationSetter component.
        obj.AddComponent<AIDestinationSetter>();
    }
}
