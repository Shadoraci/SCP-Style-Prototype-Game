using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator MyDoor = null;


    [SerializeField] private bool OpenTrigger = false;
    [SerializeField] private bool CloseTrigger = false;

    [SerializeField] private string DoorOpen = "DoorOpen";
    [SerializeField] private string DoorClose = "DoorClose";

    [SerializeField] private string KeyObject = "Player"; 

    private void OnTriggerEnter(Collider TriggerBox)
    {
        if (TriggerBox.CompareTag(KeyObject))
        {
            if (OpenTrigger)
            {
                MyDoor.Play(DoorOpen, 0, 0.0f);
                Debug.Log("Oh! The key worked!");
                gameObject.SetActive(false);
            }
            else if (CloseTrigger)
            {
                MyDoor.Play(DoorClose, 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("I need the correct key");
        }

    }
}
