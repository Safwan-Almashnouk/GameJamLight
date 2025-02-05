using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BeamRain : MonoBehaviour
{
    [SerializeField] public GameObject ray;
    [SerializeField] public GameObject dangerSign;
    [SerializeField] private Transform player;
    [SerializeField] private Transform point;

    private GameObject dangerSigns;

    void Start()
    {
        point = GameObject.Find("Roof").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }



    internal void RainAttack()
    {
        // Spawn main beam at player's position
        Vector3 playerTarget = new Vector3(player.position.x, -2, player.position.z);
        Quaternion rotation = Quaternion.Euler(0, 0 , 180) ; // Default rotation
        Instantiate(dangerSign, playerTarget, rotation);
        spawnBeam(playerTarget, rotation);

        // Spawn two additional beams at random positions
        for (int i = 0; i < 2; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPoint();
            Instantiate(dangerSign, randomPosition, rotation);
            spawnBeam(randomPosition, rotation);
            

        }
    }

    async public void spawnBeam(Vector3 spawnPoint, Quaternion rotation)
    {
       
        await Task.Delay(1000); // Delay before beam appears

#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Instantiate(ray, spawnPoint, rotation); // Spawn at exact warning position
           
        }
#endif
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(-17f, 17f); // Adjust range based on game world
        return new Vector3(randomX, -2, player.position.z);
    }
}
