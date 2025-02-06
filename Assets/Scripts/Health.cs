using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] internal int health = 100;
    private const int MAX_HEALTH = 100;

    void Update()
    {
            
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Damage amount cannot be negative");
        }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Healing amount cannot be negative");
        }

        if (health + amount > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
        else
        {
            health += amount;
        }
    }

    private void Die()
    {
        Debug.Log("I am dead!");
        Destroy(gameObject);
    }
}
