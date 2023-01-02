using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Structure
{
    #region Variables

    private Transform _spawnPoint;

    #endregion

    public override StructureType GetStructureType()
    {
        return StructureType.Military;
    }

    public void SpawnUnit()
    {
        //Get a troop from pool manager.
        GameObject troopObject = PoolManager.Instance.GetTroopObject("SoldierType1");

        if (troopObject != null)
        {
            //Get collider upper bound.
            Bounds bounds = GetComponent<BoxCollider2D>().bounds;

            //Set troop initial position as barrack lower bound position.
            troopObject.transform.position = new Vector2((bounds.center.x), (bounds.min.y));

            //Set active troop.
            troopObject.SetActive(true);

            StartCoroutine(DelayedMove(troopObject, 0.1f));
        }
    }

    private IEnumerator DelayedMove(GameObject obj, float delay)
    {
        //Wait for given delay.
        yield return new WaitForSeconds(delay);

        //Move troop to spawn point.
        obj.GetComponent<Troop>().Move(_spawnPoint);
    }

    protected override void Start()
    {
        _textObject = transform.GetChild(0).gameObject;
        _spawnPoint = transform.GetChild(1);
    }
}
