using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damage;
    private BoxCollider2D boxCollider;
    private bool canDamage = true;  // Allow multiple damages with cooldown
    private float damageCooldown = 1f;  // Time in seconds between consecutive damages

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;  // Make sure the collider is a trigger
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collider has a Health script (i.e., the player or another entity)
        if (collider.GetComponent<Health>() != null)
        {
            if (collider.CompareTag("Player"))
            {
                // Check if we can apply damage (i.e., not in cooldown)
                if (canDamage)
                {
                    Health health = collider.GetComponent<Health>();
                    health.Damage(damage);  // Apply damage to player

                    // Optional: Play a hit effect (flash red)
                    StartCoroutine(FlashRed(collider));

                    // Start the damage cooldown
                    StartCoroutine(DamageCooldown());
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with non-player objects
        if (!collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider, true);
        }
    }

    // Coroutine for damage cooldown (so player can be hit multiple times)
    private IEnumerator DamageCooldown()
    {
        canDamage = false;  // Disable damage temporarily
        yield return new WaitForSeconds(damageCooldown);  // Wait for cooldown
        canDamage = true;   // Re-enable damage
    }

    // Flash effect (color change to red and back to normal)
    private IEnumerator FlashRed(Collider2D collider)
    {
        SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;  // Flash red
        yield return new WaitForSeconds(0.1f);  // Short red flash duration
        spriteRenderer.color = Color.white;  // Revert to normal color
    }

    // If you need to reset the collision state (i.e., re-enable collisions)
    private void ResetCollision()
    {
        StartCoroutine(ReenableCollision());
    }

    private IEnumerator ReenableCollision()
    {
        yield return new WaitForSeconds(0f); // Adjust this if needed
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Physics2D.IgnoreCollision(boxCollider, col, false); // Re-enable collision
            }
        }
    }
}
