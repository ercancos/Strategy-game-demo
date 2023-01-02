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
        Structure.OnStructureSelectedAction += AssignStructureToInfoPanel;
        Troop.OnTroopSelectedAction += AssignTroopToInfoPanel;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Sets initially button's listeners.
        AddListenerToBarrackButtons();
        AddListenerToPowerPlantButtons();

        //Set information panel to inactive
        SetStateSelectedObjectInformationPanel(false);
        //Set button to inactive
        SetStateSelectedObjectTroopButton(false);
    }

    private void OnDestroy()
    {
        Structure.OnStructureSelectedAction -= AssignStructureToInfoPanel;
        Troop.OnTroopSelectedAction -= AssignTroopToInfoPanel;
    }

    /*
    private void AssignSelectedObjectToInfoPanel(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.CompareTag("Structure"))
            {

            }
            else if (obj.CompareTag("Troop"))
            {

            }
            else
            {
                Debug.LogWarning("Object could not identify !");
            }
        }

        selectedObjectInformationPanel.SetActive(true);
    }
    */

    private void AssignStructureToInfoPanel(GameObject obj)
    {
        Structure structureType = obj.GetComponent<Structure>();
        if (structureType != null)
        {
            if (structureType is Barrack)
            {
                AssignText("Barrack");
                AssignSprite(obj.GetComponent<SpriteRenderer>().sprite);
                AddListenerToSelectedObjectTroopButton(obj.GetComponent<Barrack>());
                SetStateSelectedObjectTroopButton(true);
            }
            else if (structureType is PowerPlant)
            {
                SetStateSelectedObjectTroopButton(false);
                AssignText("PowerPlant");
                AssignSprite(obj.GetComponent<SpriteRenderer>().sprite);
            }
            else
            {
                Debug.LogWarning("Object could not identify !");
            }

            SetStateSelectedObjectInformationPanel(true);
        }
    }

    private void AssignTroopToInfoPanel(GameObject obj)
    {
        Troop troopType = obj.GetComponent<Troop>();
        if (troopType != null)
        {
            if (troopType is Soldier)
            {
                SetStateSelectedObjectTroopButton(false);
                Debug.Log("Soldier");
            }
            else
            {
                Debug.LogWarning("Object could not identify !");
            }

            SetStateSelectedObjectInformationPanel(true);
        }
    }

    private void AssignText(string text)
    {
        selectedObjectText.text = text;
    }

    private void AssignSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            selectedObjectImage.sprite = sprite;
        }
    }

    private void AddListenerToSelectedObjectTroopButton(Barrack barrack)
    {
        if (barrack != null)
        {
            Button button = selectedObjectTroopButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(barrack.SpawnUnit);
        }
    }


    private void SetStateSelectedObjectTroopButton(bool state)
    {
        selectedObjectTroopButton.SetActive(state);
    }

    private void SetStateSelectedObjectInformationPanel(bool state)
    {
        selectedObjectInformationPanel.SetActive(state);
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
