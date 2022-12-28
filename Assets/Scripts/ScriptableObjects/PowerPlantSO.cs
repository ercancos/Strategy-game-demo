using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structures/Energy/PowerPlant")]
public class PowerPlantSO : StructureDataSO
{
    /*
    public override GameObject InstantiateStructure(Vector3 pos)
    {
        GameObject createdObject = Instantiate(new GameObject(), pos, Quaternion.identity);
        createdObject.AddComponent<SpriteRenderer>();
        createdObject.AddComponent<Rigidbody2D>();
        createdObject.AddComponent<BoxCollider2D>();
        createdObject.transform.localScale = new Vector3(width, height, 1);

        return createdObject;
    }
    */
}
