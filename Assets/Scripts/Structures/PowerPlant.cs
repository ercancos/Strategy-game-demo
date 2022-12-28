using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Structure
{
    public override StructureType GetStructureType()
    {
        return StructureType.Energy;
    }
}
