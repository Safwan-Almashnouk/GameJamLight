using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update

   
    public float timer;
    public float Rof;
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if (timer > Rof)
        {
           
            timer = 0;
            Destroy(gameObject); 
        }
    }

    
}

