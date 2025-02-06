using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image HealthImage;  // Reference to the UI Image that represents the health bar
    public Health health;      // Reference to the Health script
    public int MAX_HEALTH = 50;  // You can make this a constant or use Health.MAX_HEALTH if you want to reference it from the Health script

    void Start()
    {
        // Make sure to initialize the health bar properly on start
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously update the health bar in each frame
        UpdateHealthBar();
    }

    // Method to update the health bar based on the current health
    private void UpdateHealthBar()
    {
        // Set the fill amount of the health bar based on the health percentage
        HealthImage.fillAmount = (float)health.health / MAX_HEALTH;
    }
}
