using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    [SerializeField]
    protected PlacedObjectDataSO objectData;

    //Create a structure depending on the concrete-class factory implementation.
    public abstract GameObject CreateObject(Vector2 pos);

}
