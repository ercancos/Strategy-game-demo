//Libraries..
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script is responsible for management of pool system.
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

        if (_structurePoolDictionary[tag].Count <= 0)
        {
            string barrackTag = barrackFactory.ObjectData.GetName;
            string powerPlantTag = powerplantFactory.ObjectData.GetName;
            if (tag == barrackTag)
            {
                SpawnBarrack();
            }
            else if (tag == powerPlantTag)
            {
                SpawnPowerPlant();
            }
        }

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

        if (_troopPoolDictionary[tag].Count <= 0)
        {
            SpawnSoldier();
        }

        GameObject troopObject = _troopPoolDictionary[tag].Dequeue();
        //troopObject.SetActive(true);
        return troopObject;
    }

    //This function recycles the given object(if it is exist) and puts it back in the pool.
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
        string barrackTag = barrackFactory.ObjectData.GetName;
        Queue<GameObject> objectPool = new Queue<GameObject>();
        _structurePoolDictionary.Add(barrackTag, objectPool);

        string powerPlantTag = powerplantFactory.ObjectData.GetName;
        objectPool = new Queue<GameObject>();
        _structurePoolDictionary.Add(powerPlantTag, objectPool);

        //Generate relevant type of troops.
        string soldierTag = ((BarrackFactory)barrackFactory).SpawnableTroopData.GetName;
        objectPool = new Queue<GameObject>();
        _troopPoolDictionary.Add(soldierTag, objectPool);


        for (int i = 0; i < initiallySpawnedObjectNumber; i++)
        {
            SpawnBarrack();
            SpawnPowerPlant();
            SpawnSoldier();
        }
    }

    private void SpawnBarrack()
    {
        GameObject createdStructure = barrackFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        string barrackTag = barrackFactory.ObjectData.GetName;
        _structurePoolDictionary[barrackTag].Enqueue(createdStructure);
    }

    private void SpawnPowerPlant()
    {
        GameObject createdStructure = powerplantFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        string powerPlantTag = powerplantFactory.ObjectData.GetName;
        _structurePoolDictionary[powerPlantTag].Enqueue(createdStructure);
    }

    private void SpawnSoldier()
    {
        GameObject createdStructure = soldierFactory.CreateObject(Vector3.zero);
        createdStructure.SetActive(false);
        string soldierTag = ((BarrackFactory)barrackFactory).SpawnableTroopData.GetName;
        _troopPoolDictionary[soldierTag].Enqueue(createdStructure);
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
                string barrackTag = barrackFactory.ObjectData.GetName;
                _structurePoolDictionary[barrackTag].Enqueue(obj);
            }
            else if (structureType is PowerPlant)
            {
                obj.SetActive(false);
                obj.transform.position = Vector3.zero;
                string powerPlantTag = powerplantFactory.ObjectData.GetName;
                _structurePoolDictionary[powerPlantTag].Enqueue(obj);
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
            string soldierTag = ((BarrackFactory)barrackFactory).SpawnableTroopData.GetName;
            _troopPoolDictionary[soldierTag].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Object could not identify. obj will be destroyed !");
            Destroy(obj);
        }
    }

}


