using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structures/Military/Barrack")]
public class BarrackSO : PlacedObjectDataSO
{
    #region Variables

    [Header("OTHER PROPERTIES")]
    [SerializeField]
    [Tooltip("This is the visual that represents the spawn point of units. A sprite.")]
    private Sprite spawnPointIndicator;

    #endregion

    //Getter methods.
    public Sprite SpawnPointIndicator { get => spawnPointIndicator; }
}
