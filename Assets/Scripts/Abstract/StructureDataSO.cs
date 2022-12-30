using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlacedObjectDataSO : ScriptableObject
{
    [SerializeField]
    protected string visualTextName;

    [SerializeField]
    protected Sprite sprite;

    [SerializeField]
    protected int width;

    [SerializeField]
    protected int height;


    //Getter methods.
    public Sprite GetSprite => sprite;
    public int GetWidth => width;
    public int GetHeight => height;
    public string GetName => visualTextName;
}
