using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public IndividualItem Item;
    private PlayerMovement PlayerReference; 

    public Transform KeyLocationReference; 

    public void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        //(Item.name != "Key1" || Item.name != "Key2" || Item.name != "Key3" || Item.name != "Key4" || Item.name != "Key5" || Item.name != "Key6")
        if (gameObject.layer != 6)
        {
            Destroy(gameObject);
        }
        else if(gameObject.layer == 6)
        {
            gameObject.transform.SetParent(KeyLocationReference);
            gameObject.transform.position = KeyLocationReference.position;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("I found a key!");
        }
        
    }
    private void OnMouseDown()
    {
        PickUp();
    }
}
