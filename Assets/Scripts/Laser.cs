using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    private Transform PPos;

    [SerializeField] private float initialDelay;
    [SerializeField] private float followDuration;
    RaycastHit2D[] hits;
    private AudioSource audioSource;
    private float originalVolume;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        PPos = GameObject.Find("Player").GetComponent<Transform>();

        m_lineRenderer.enabled = false;
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
    }

    public void ShootLaser()
    {

        StartCoroutine(ShootAndFollowLaser());
    }

    private void Update()
    {
        Debug.Log(audioSource.volume);
    }
    private IEnumerator ShootAndFollowLaser()
    {
        Vector2 playerInitialPos = PPos.position;

        m_lineRenderer.enabled = true;
        yield return new WaitForSeconds(initialDelay);
        Draw2DRay(laserFirePoint.position, playerInitialPos);

        float elapsedTime = 0f;
        while (elapsedTime < followDuration)
        {
            Vector2 playerPos = PPos.position;
            Vector2 laserDirection = (playerInitialPos - (Vector2)laserFirePoint.position).normalized;
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, laserDirection);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Boss")) // Replace with actual tag
                {
                    continue; // Skip this object
                }
                if (_hit)
                {
                    Draw2DRay(laserFirePoint.position, _hit.point);

                }
                else
                {
                    Draw2DRay(laserFirePoint.position, playerInitialPos);

                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        void Draw2DRay(Vector2 startPos, Vector2 endPos)
        {

            m_lineRenderer.SetPosition(0, startPos);
            m_lineRenderer.SetPosition(1, endPos);
            audioSource.volume = originalVolume;
            audioSource.Play();

            Invoke("Destroy", 0.5f); // Waits 2 seconds before calling DoSomething()

        }

       
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    private void Destroy()
    {
        m_lineRenderer.enabled = false;
        StartCoroutine(StartFade(audioSource, 0.5f, 0));
        
    }
}
