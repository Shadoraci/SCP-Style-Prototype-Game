using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [Header("Gun Properties")]
    public WeaponScript GunScript;
    public Rigidbody RB;
    public BoxCollider Collider;
    public Transform Player, GunContainer, FPSCam;

    [Header("Pickup Properties")]
    public float PickUpRange;
    public float DropForwardForce, DropUpwardForce;

    [Header("Player Properties")]
    public bool Equipped;
    public static bool SlotFull;

    private void Start()
    {
        //Setup 
        if (!Equipped)
        {
            GunScript.enabled = false;
            RB.isKinematic = false; 
            Collider.isTrigger = false;

        }
        if (Equipped)
        {
            GunScript.enabled = true;
            RB.isKinematic = true;
            Collider.isTrigger = true;
            SlotFull = true; 
        }
    }
    private void Update()
    {
        //Checking if player is in range of E
        Vector3 DistanceToPlayer = Player.position - transform.position; 
        if (!Equipped && DistanceToPlayer.magnitude < PickUpRange && Input.GetKeyDown(KeyCode.E) && !SlotFull)
        {
            PickUp(); 
        }

        //Drop if equipped and Q is pressed
        if(Equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop(); 
        }
    }
    private void PickUp()
    {
        Equipped = true;
        SlotFull = true;

        //Make weapon a child of the camera and move it to default pos
        transform.SetParent(GunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero); 
        transform.localScale = Vector3.one;

        //Make rigidbody kinematic and collider a trigger
        RB.isKinematic = true;
        Collider.isTrigger = true;

        //enable firing script
        GunScript.enabled = true; 
    }
    private void Drop()
    {
        Equipped = false;
        SlotFull = false;

        //Set parent to null
        transform.SetParent(null); 

        //Make rigidbody not kinematic and collider normal
        RB.isKinematic = false;
        Collider.isTrigger = false;

        //Gun carries momentum of player
        RB.velocity = Player.GetComponent<Rigidbody>().velocity;

        //Addforce
        RB.AddForce(FPSCam.forward * DropForwardForce, ForceMode.Impulse);
        RB.AddForce(FPSCam.up * DropUpwardForce, ForceMode.Impulse);
        //Add funny spin
        float random = Random.Range(-1f, 1f);
        RB.AddTorque(new Vector3(random, random, random) * 10); 


        //enable firing script
        GunScript.enabled = false;
    }
}
