using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Structure
{
    public override StructureType GetStructureType()
    {
        return StructureType.Military;
    }
}
