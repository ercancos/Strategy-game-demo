using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Structure
{
    #region Variables

    private Transform _spawnPoint;
    private PlacedObjectDataSO spawnableTroopData;

    #endregion

    public PlacedObjectDataSO SpawnableTroopData { get => spawnableTroopData; set => spawnableTroopData = value; }

    //Returns structure type.
    public override StructureType GetStructureType()
    {
        return StructureType.Military;
    }

    //Spawns a unit by using pool manager.
    public void SpawnUnit()
    {
        //Get a troop from pool manager.
        GameObject troopObject = PoolManager.Instance.GetTroopObject(spawnableTroopData.GetName);

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

    //Moves given object after given delay.
    private IEnumerator DelayedMove(GameObject obj, float delay)
    {
        //Wait for given delay.
        yield return new WaitForSeconds(delay);

        //Move troop to spawn point.
        obj.GetComponent<Troop>().MoveTo(_spawnPoint);
    }

    protected override void Start()
    {
        //Get text object.
        _textObject = transform.GetChild(0).gameObject;
        _textObject.SetActive(false);

        //Get spawn-point object.
        _spawnPoint = transform.GetChild(1);
    }
}
