using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damage;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.GetComponent<Health>() != null)
        {
            
            if (collider.CompareTag("Player"))
            {
                Health health = collider.GetComponent<Health>();
                health.Damage(damage);

                
                Physics2D.IgnoreCollision(boxCollider, collider, true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider, true);
        }
    }

    
    private void ResetCollision()
    {
        
        StartCoroutine(ReenableCollision());
    }

    private IEnumerator ReenableCollision()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Physics2D.IgnoreCollision(boxCollider, col, false); // Re-enable collision with player
            }
        }
    }
}
