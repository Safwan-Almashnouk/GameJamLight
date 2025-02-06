using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAttack : MonoBehaviour
{
    public GameObject ray;              
    public float speed = 5f;            
    public float spawnDelay = 0.5f;     
    public Transform[] waypoints;      
    private int waypointIndex = 0;    
    public float extraDelay = 1f;
    private bool isAttacking;



    internal void TidalWave()
    {
        StartCoroutine(MoveTowardsWaypoint());
    }

   
    IEnumerator MoveTowardsWaypoint()
    {
        isAttacking = true;
        Transform target = waypoints[waypointIndex];
        StartCoroutine(spawnBeam());

        while (Vector2.Distance(transform.position, target.position) >= 1f)
        {
          
            transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.deltaTime);
            yield return null;   
        }
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        isAttacking = false;
        

    }

   
    IEnumerator spawnBeam()
    {

        while (isAttacking == true)  // Loop indefinitely until the movement stops
        {
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(ray, transform.position, rotation);
            yield return new WaitForSeconds(spawnDelay);  // Wait for the next spawn
        }
    }
}
