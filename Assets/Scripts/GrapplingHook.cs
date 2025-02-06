using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 5f; // Distance the player stays from grapple point
    [SerializeField] private float maxGrappleRange = 10f; // Maximum grapple detection range
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        // When the player clicks the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get direction from player to mouse position
            Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;

            // Debug the Raycast direction (optional)
            Debug.DrawRay(transform.position, direction * maxGrappleRange, Color.red, 1f); // Debug Ray

            // Perform the raycast from player position to mouse position
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxGrappleRange, grappleLayer);

            if (hit.collider != null)
            {
                // If we hit an object in the grapple layer, set the grapple point
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;

                // Update the rope's positions
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }
            else
            {
                // If no valid target found, print a debug message
                Debug.Log("No valid grapple target found.");
            }
        }

        // When the player releases the mouse button
        if (Input.GetMouseButtonUp(0))
        {
            // Disable the joint and rope
            joint.enabled = false;
            rope.enabled = false;
        }

        // Keep the rope position updated as the player moves
        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position); // Follow the player's position
        }
    }
}