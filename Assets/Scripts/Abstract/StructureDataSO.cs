//Libraries..
using UnityEngine;

/*
 * 
 * 
 * 
 */

public abstract class PlacedObjectDataSO : ScriptableObject
{
    #region Variables

    [Header("OBJECT PROPERTIES")]
    [SerializeField]
    [Tooltip("This is the name that appears in the game on top of the object.")]
    protected string visualTextName;

    [SerializeField]
    [Tooltip("This is main visual that represents the object. A sprite.")]
    protected Sprite sprite;

    [Space]
    [Header("OBJECT DIMENSIONS")]
    [SerializeField]
    [Tooltip("This is the width value of object.")]
    protected int width;

    [SerializeField]
    [Tooltip("This is the height value of object.")]
    protected int height;

    #endregion

    //Getter methods.
    public Sprite GetSprite => sprite;
    public int GetWidth => width;
    public int GetHeight => height;
    public string GetName => visualTextName;
}
