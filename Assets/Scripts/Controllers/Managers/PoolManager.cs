//Libraries..
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 * 
 */

public class PoolManager : MonoBehaviour
{
    #region Variables
    public static PoolManager Instance;

    [SerializeField]
    private int initiallySpawnedObjectNumber = 10;

    [SerializeField]
    private Factory barrackFactory;
    [SerializeField]
    private Factory powerplantFactory;
    [SerializeField]
    private Factory soldierFactory;

    private Dictionary<string, Queue<GameObject>> _structurePoolDictionary;
    private Dictionary<string, Queue<GameObject>> _troopPoolDictionary;

    #endregion

    //It returns a structure gameobject according to given input(tag).
    public GameObject GetStructureObject(string tag)
    {
        if (!_structurePoolDictionary.ContainsKey(tag))
            return null;

        GameObject structureObject = _structurePoolDictionary[tag].Dequeue();
        //Move object to latest mouse position.
        structureObject.transform.position = PlayerInteractionController.Instance.GetLatestMousePos();
        //Set object to active.
        structureObject.SetActive(true);

        return structureObject;
    }

    //It returns a troop gameobject according to given input(tag).
    public GameObject GetTroopObject(string tag)
    {
        if (!_troopPoolDictionary.ContainsKey(tag))
            return null;

        GameObject troopObject = _structurePoolDictionary[tag].Dequeue();
        troopObject.SetActive(true);
        return troopObject;
    }

    //This function recycles the given object and puts it back in the pool.
    public void RecycleObject(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.CompareTag("Structure"))
            {
                RecycleStructure(obj);
            }
            else if (obj.CompareTag("Troop"))
            {
                RecycleTroop(obj);
            }
            else
            {
                Debug.LogWarning("Object could not identify. obj will be destroyed !");
                Destroy(obj);
            }
        }
        else
        {
            Debug.LogError("Recycle operation could not complete. obj is null !");
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _structurePoolDictionary = new Dictionary<string, Queue<GameObject>>();
        _troopPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        Initialize();
    }

    private void Initialize()
    {
        //Generate relevant type of structures.
        Queue<GameObject> objectPool = new Queue<GameObject>();
        _structurePoolDictionary.Add("BarrackType1", objectPool);

        objectPool = new Queue<GameObject>();
        _structurePoolDictionary.Add("PowerPlant", objectPool);

        //Generate relevant type of troops.
        objectPool = new Queue<GameObject>();
        _troopPoolDictionary.Add("SoldierType1", objectPool);


        for (int i = 0; i < initiallySpawnedObjectNumber; i++)
        {
            SpawnBarrackType1();
            SpawnPowerPlant();
            SpawnSoldierType1();
        }
    }

    private void SpawnBarrackType1()
    {
        GameObject createdStructure = barrackFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        _structurePoolDictionary["BarrackType1"].Enqueue(createdStructure);
    }

    private void SpawnPowerPlant()
    {
        GameObject createdStructure = powerplantFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        _structurePoolDictionary["PowerPlant"].Enqueue(createdStructure);
    }

    private void SpawnSoldierType1()
    {
        GameObject createdStructure = soldierFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        _troopPoolDictionary["SoldierType1"].Enqueue(createdStructure);
    }

    private void RecycleStructure(GameObject obj)
    {
        Structure structureType = obj.GetComponent<Structure>();
        if (structureType != null)
        {
            if (structureType is Barrack)
            {
                obj.SetActive(false);
                obj.transform.position = Vector3.zero;
                _structurePoolDictionary["BarrackType1"].Enqueue(obj);
            }
            else if (structureType is PowerPlant)
            {
                obj.SetActive(false);
                obj.transform.position = Vector3.zero;
                _structurePoolDictionary["PowerPlant"].Enqueue(obj);
            }
            else
            {
                Debug.LogWarning("Object could not identify. obj will be destroyed !");
                Destroy(obj);
            }
        }
    }

    private void RecycleTroop(GameObject obj)
    {
        Troop troopType = obj.GetComponent<Troop>();
        if (troopType is Soldier)
        {
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
            _troopPoolDictionary["SoldierType1"].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Object could not identify. obj will be destroyed !");
            Destroy(obj);
        }
    }

}


