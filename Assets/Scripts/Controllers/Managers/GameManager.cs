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
        Debug.Log("Barrack created.");
    }

    public void CreatePowerPlant()
    {
        StructureBuildController.Instance.TakeStructureToBuilt(PoolManager.Instance.GetStructureObject("PowerPlant"));
        Debug.Log("PowerPlant created.");
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

    private void UpdateAStar()
    {
        astarPath.Scan();
    }

    private void OnDestroy()
    {
        StructureBuildController.OnStructureBuildAction -= UpdateAStar;
    }

}