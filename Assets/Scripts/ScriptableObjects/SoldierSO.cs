using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Troops/Military/Soldier")]
public class SoldierSO : PlacedObjectDataSO
{
    #region Variables

    [Header("OTHER PROPERTIES")]
    [SerializeField]
    [Tooltip("This is the visual that appears when player selects the object. A sprite.")]
    private Sprite selectedOutline;

    [SerializeField]
    [Tooltip("This is the visual that appears when the player moves the object to a target location. A sprite.")]
    private Sprite targetPosition;

    #endregion

    //Getter methods.
    public Sprite SelectedOutline { get => selectedOutline; }
    public Sprite TargetPosition { get => targetPosition; }
}
