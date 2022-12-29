using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Troop
{
    public override TroopType GetTroopType()
    {
        return TroopType.Military;
    }
}
