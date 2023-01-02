using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structures/Military/Barrack")]
public class BarrackSO : PlacedObjectDataSO
{
    [SerializeField]
    private Sprite spawnPointIndicator;

    public Sprite SpawnPointIndicator { get => spawnPointIndicator; }
}
