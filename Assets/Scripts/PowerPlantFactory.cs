using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantFactory : StructureFactory
{
    public override GameObject CreateStructure(Vector2 pos)
    {
        GameObject createdObject = Instantiate(
            new GameObject(),
            pos,
            Quaternion.identity);

        SpriteRenderer createdObjectSpriteRenderer = createdObject.AddComponent<SpriteRenderer>();
        createdObjectSpriteRenderer.sprite = structureData.GetSprite;
        createdObject.AddComponent<Rigidbody2D>();
        createdObject.AddComponent<BoxCollider2D>();
        createdObject.transform.localScale = new Vector3(structureData.GetWidth, structureData.GetHeight, 1);

        createdObject.AddComponent<PowerPlant>();

        return createdObject;
    }
}
