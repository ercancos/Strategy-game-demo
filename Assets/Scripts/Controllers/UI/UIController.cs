//Libraries..
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 
 * 
 */

public class UIController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private List<Button> barracksButtons = new List<Button>();
    [SerializeField]
    private List<Button> powerPlantButtons = new List<Button>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Sets initially button's listeners.
        AddListenerToBarrackButtons();
        AddListenerToPowerPlantButtons();
    }

    //Adds a listener function to relevant button.
    public void AddListenerToBarrackButtons()
    {
        foreach (Button button in barracksButtons)
        {
            button.onClick.AddListener(GameManager.Instance.CreateBarrack);
        }
    }

    //Adds a listener function to relevant button.
    public void AddListenerToPowerPlantButtons()
    {
        foreach (Button button in powerPlantButtons)
        {
            button.onClick.AddListener(GameManager.Instance.CreatePowerPlant);
        }
    }
}
