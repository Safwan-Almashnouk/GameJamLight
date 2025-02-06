using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed = 2f; // Added default speed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        // Move the object towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

        // Check if the object is close enough to switch directions
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            flip();
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        }
    }

    void flip()
    {
        // Flip the object's sprite direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}