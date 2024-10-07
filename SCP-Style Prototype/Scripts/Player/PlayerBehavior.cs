using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEditor;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;
    public int Health;

    public TMP_Text HealthbarText;
    public Slider HealthbarSlider;

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {  
        HealthbarSlider.value = Health;
        HealthbarText.text = $"HP: {Health}";
    }
    public void IncreaseHealth(int HealthIntake)
    {
        //HealthIntake can have negative values for damage
        Health += HealthIntake;
        HealthbarText.text = $"HP: {Health}";

        Debug.Log("Health has been changed!");
    }
    public void Update()
    {
        HealthbarText.text = $"HP: {Health}";
        HealthbarSlider.value = Health;
        //Resetting Health if over
        if (Health >= 100)
        {
            Health = 100; 
        }
        else if (Health <= 0)
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None; 
            //EndGame probably controlled in GameManager
        }
    }
    //------------------------------DamageDetection--------------------------------------
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IncreaseHealth(-20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            IncreaseHealth(-10);
        }
    }
}
