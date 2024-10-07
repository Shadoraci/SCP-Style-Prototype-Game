using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Bullet Type")]
    public GameObject Bullet;

    [Header("Forces")]
    public float ShootForce, UpwardForce;

    [Header("Gun Statistics")]
    public float TimeBetweenShooting, Spread, ReloadTime, TimeBetweenShots;
    public int MagazineSize, BulletsPerTap;
    public bool AllowButtonHold;

    private int BulletsLeft, BulletsShot;

    //Bools
    bool Shooting, ReadyToShoot, Reloading;

    [Header("References")]
    public Camera FPSCamera;
    public Transform AttackPoint; //BulletSpawn

    [Header("Graphics")]
    public GameObject MuzzleFlash;
    public TextMeshProUGUI AmmunitionDisplay;  
    //Bug Fixing
    public bool AllowInvoke = true;

    private void Awake()
    {
        //Magazine Filling
        BulletsLeft = MagazineSize;
        ReadyToShoot = true; 
    }
    private void Update()
    {
        MyInput(); 

        //Set Ammo Display, if it exists
        if (AmmunitionDisplay != null)
        {
            AmmunitionDisplay.SetText(BulletsLeft / BulletsPerTap + " / " + MagazineSize / BulletsPerTap); 
        }
    }
    private void MyInput()
    {
        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < MagazineSize && !Reloading)
        {
            Reload(); 
        }
        if (ReadyToShoot && Shooting && !Reloading && BulletsLeft <= 0)
        {
            Reload(); 
        }
        //Check input parameters
        if (AllowButtonHold)
        {
            //GetKey allows hold
            Shooting = Input.GetKey(KeyCode.Mouse1);
        }
        else
        {
            //GetKeyDown does not allow hold
            Shooting = Input.GetKeyDown(KeyCode.Mouse1);
        }
        //Shooting
        if(ReadyToShoot && Shooting && !Reloading && BulletsLeft > 0)
        {
            //Set BulletsShot to 0
            BulletsShot = 0;

            Shoot(); 
        }
    }
    private void Shoot()
    {
        ReadyToShoot = false;

        //Find exact hit position using raycast
        Ray RayCast = FPSCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit RayHit;

        //Check if Ray has hit something
        Vector3 TargetPoint; 
        if(Physics.Raycast(RayCast, out RayHit))
        {
            TargetPoint = RayHit.point;
        }
        else
        {
            //This assumes player is shooting in the sky AKA not hitting anything at all
            TargetPoint = RayCast.GetPoint(75); //A point far away from player
        }
        //Calculate the direction from attackpoint to targetpoint
        Vector3 DirectionWithoutSpread = TargetPoint - AttackPoint.position;


        //Calculate Spread
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        //Calculate new direction with spread
        Vector3 DirectionWithSpread = DirectionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate Bullet/Projectile
        GameObject CurrentBullet = Instantiate(Bullet, AttackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction 
        CurrentBullet.transform.forward = DirectionWithSpread.normalized;

        //Add forces to bullet
        CurrentBullet.GetComponent<Rigidbody>().AddForce(DirectionWithSpread.normalized * ShootForce, ForceMode.Impulse);
        //UpwardForce
        CurrentBullet.GetComponent<Rigidbody>().AddForce(FPSCamera.transform.up * UpwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash
        if (MuzzleFlash != null)
        {
            GameObject Muzzle = Instantiate(MuzzleFlash, AttackPoint.position, AttackPoint.rotation);
            Destroy(Muzzle, 0.5f); 
        }

        BulletsLeft--;
        BulletsShot++; 

        //Invoke ResetShot function (if not already invoked)
        if (AllowInvoke)
        {
            Invoke("ResetShot", TimeBetweenShooting);
            AllowInvoke = false; 
        }

        //If I wanted more than one bullet per tap, this function should repeat
        if (BulletsShot < BulletsPerTap && BulletsLeft > 0)
        {
            Invoke("Shoot", TimeBetweenShots);
        }
    }
    private void ResetShot()
    {
        ReadyToShoot = true;
        AllowInvoke = true; 
    }
    private void Reload()
    {
        Reloading = true;
        Invoke("ReloadFinished", ReloadTime); 
    }
    private void ReloadFinished()
    {
        BulletsLeft = MagazineSize;
        Reloading = false;
    }
}
