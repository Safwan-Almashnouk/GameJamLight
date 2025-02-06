using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public Health health;
    void Start()
    {
        Health health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health <= 0)
        {
            Invoke("Retry" ,2f);
        }
    }

    void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
