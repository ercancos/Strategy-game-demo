using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Troop : MonoBehaviour
{
    public enum TroopType
    {
        Military,
        FirstAid,
        Citizen
    }

    public abstract TroopType GetTroopType();
}
