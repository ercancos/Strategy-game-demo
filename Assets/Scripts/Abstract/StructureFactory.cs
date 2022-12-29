using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureFactory : MonoBehaviour
{
    [SerializeField]
    protected PlacedObjectDataSO structureData;

    //Create a structure depending on the concrete-class factory implementation.
    public abstract GameObject CreateStructure(Vector2 pos);

}
