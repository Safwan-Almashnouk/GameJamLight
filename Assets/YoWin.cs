using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YoWin : MonoBehaviour
{
    public Health health;
    void Start()
    {
        Health health = GetComponent<Health>();
    }


    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0)
        {
            Invoke("Win", 0f);
        }
    }

    void Win()
    {
       ;
        SceneManager.LoadScene("YouWin");

    }
}
