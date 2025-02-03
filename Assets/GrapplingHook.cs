using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
            origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
            direction: Vector2.zero,
            distance: Mathf.Infinity,
            layerMask: grappleLayer
            );

            if(hit.collider !=null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
        }
    }
}
