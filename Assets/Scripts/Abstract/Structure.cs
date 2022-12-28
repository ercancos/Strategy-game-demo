using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public enum StructureType
    {
        Military,
        Energy
    }

    public abstract StructureType GetStructureType();

    protected StructureDataSO structureData;
}
