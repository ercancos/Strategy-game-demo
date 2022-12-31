//Libraries..
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 
 * 
 * 
 */

public class UIController : MonoBehaviour
{
    #region Variables

    public static UIController Instance;

    [Header("Information Panel")]

    [SerializeField]
    private GameObject selectedObjectInformationPanel;

    [SerializeField]
    private TextMeshProUGUI selectedObjectText;

    [SerializeField]
    private Image selectedObjectImage;

    [SerializeField]
    private GameObject selectedObjectTroopButton;

    [Space]
    [Space]
    [Header("Production Menu")]

    [SerializeField]
    private List<Button> barracksButtons = new List<Button>();

    [SerializeField]
    private List<Button> powerPlantButtons = new List<Button>();

    #endregion

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
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Sets initially button's listeners.
        AddListenerToBarrackButtons();
        AddListenerToPowerPlantButtons();

        //Set information panel to inactive
        selectedObjectInformationPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        
    }

    private void AssignSelectedObjectToInfoPanel()
    {

    }

    //Adds a listener function to relevant button.
    private void AddListenerToBarrackButtons()
    {
        foreach (Button button in barracksButtons)
        {
            button.onClick.AddListener(GameManager.Instance.CreateBarrack);
        }
    }

    //Adds a listener function to relevant button.
    private void AddListenerToPowerPlantButtons()
    {
        foreach (Button button in powerPlantButtons)
        {
            button.onClick.AddListener(GameManager.Instance.CreatePowerPlant);
        }
    }
}
