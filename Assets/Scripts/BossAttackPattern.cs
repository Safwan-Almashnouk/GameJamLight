using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern : MonoBehaviour
{
    
    
    private bool DoneAttacking = true;
    public float timer;
    private BeamRain Br;
    
    void Start()
    {
      Br = GetComponent<BeamRain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoneAttacking == true)
        {
            AttackPattern();
        }
    }


    void AttackPattern()
    {
        int[] options = { 10, 20, 30 }; // Your three choices
        int randomChoice = options[Random.Range(0, options.Length)];
        
        if (randomChoice == 10)
        {

            RainBeam();
            DoneAttacking = false;
        }
        if (randomChoice == 20) 
        {
            RainBeam();
            DoneAttacking = false;
        }
        if(randomChoice == 30)
        {
            RainBeam(); 
            DoneAttacking = false;
        }
       
    }

     void RainBeam()
    {
        Br.RainAttack();
        StartCoroutine(Cooldown());
    }

    void SunPattern()
    {

    }

    void BeamAttack()
    {

    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
        DoneAttacking = true;
    }
}
