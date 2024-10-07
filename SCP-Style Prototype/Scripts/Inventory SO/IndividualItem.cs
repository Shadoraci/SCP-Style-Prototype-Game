using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class IndividualItem : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public int Value;
    public Sprite Icon;
    public ItemType itemType;

    //For changeability in the future if decided
    public enum ItemType
    {
       Healingitem,
       Key,
       Misc
    }
}
