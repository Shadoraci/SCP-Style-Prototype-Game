using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    IndividualItem Item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(Item);
        Debug.Log("Item was removed after use!");
        Destroy(gameObject); 
    }
    public void AddItem(IndividualItem newItem)
    {
        Item = newItem; 
    }
    public void UseItem()
    {
        Debug.Log("Item is type " + Item.itemType);

        switch (Item.itemType)
        {
            case IndividualItem.ItemType.Healingitem:
                Debug.Log("Item is being used");
                Debug.Log("Item Value is " + Item.Value);
                PlayerBehavior.Instance.IncreaseHealth(Item.Value);
                break;

            case IndividualItem.ItemType.Key:
                //Does nothing when button is clicked. Purely visual. 
                break;
        }
        RemoveItem(); 
    }
}
