using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public LinkedList<IndividualItem> Items = new LinkedList<IndividualItem>();

    public Transform ItemContent;
    public GameObject InventoryItems;

    public ItemUseController[] InventoryItemsArr; 

    private void Awake()
    {
        Instance = this;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ListItems();
        }
    }
    public void Add(IndividualItem Item)
    {
        Items.AddFirst(Item); 
    }
    public void Remove(IndividualItem Item)
    {
        Items.Remove(Item); 
    }
    public void ListItems()
    {
        //Cleans duplication
        foreach (Transform Item in ItemContent)
        {
            Destroy(Item.gameObject); 
        }
        foreach(var Item in Items)
        {
            GameObject Object = Instantiate(InventoryItems, ItemContent);
            var ItemName = Object.transform.Find("ItemName").GetComponent<TMP_Text>();
            var ItemIcon = Object.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = Item.ItemName;
            ItemIcon.sprite = Item.Icon; 
        }
        SetInventoryItems(); 
    }
    
    public void SetInventoryItems()
    {
        InventoryItemsArr = ItemContent.GetComponentsInChildren<ItemUseController>(); 

        for(int i = 0; i < Items.Count; i++)
        {
            InventoryItemsArr[i].AddItem(Items.ElementAt(i));
        }
    }
}
