//Libraries..
using UnityEngine;

/*
 * 
 * This script is responsible for management of game. 
 * 
 */

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance;

    [SerializeField]
    private AstarPath astarPath;


    #endregion

    //Getter methods.
    //..


    public void CreateBarrack()
    {
        StructureBuildController.Instance.TakeStructureToBuilt(PoolManager.Instance.GetStructureObject("BarrackType1"));
    }

    public void CreatePowerPlant()
    {
        StructureBuildController.Instance.TakeStructureToBuilt(PoolManager.Instance.GetStructureObject("PowerPlant"));
    }

    public void CreateSoldier()
    {

        Debug.Log("Soldier created.");
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

    private void OnEnable()
    {
        StructureBuildController.OnStructureBuildAction += UpdateAStar;
    }

    private void OnDestroy()
    {
        StructureBuildController.OnStructureBuildAction -= UpdateAStar;
    }

    private void UpdateAStar()
    {
        astarPath.Scan();
    }

}