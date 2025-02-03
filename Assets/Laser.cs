using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private Transform PPos;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        PPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector2 playerPos = PPos.position;

        if (Physics2D.Raycast(laserFirePoint.position, (playerPos - (Vector2)laserFirePoint.position).normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, (playerPos - (Vector2)laserFirePoint.position).normalized);
            Draw2DRay(laserFirePoint.position, _hit.point); 
        }
        else
        {
            Draw2DRay(laserFirePoint.position, playerPos);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

}
