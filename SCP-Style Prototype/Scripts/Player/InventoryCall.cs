using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryCall : MonoBehaviour
{
    public GameObject InventoryGUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryGUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            InventoryGUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
