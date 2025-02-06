using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern : MonoBehaviour
{
    
    
    private bool DoneAttacking = true;
    public float timer;
    private BeamRain Br;
    public Laser beam;
    public SunAttack Wave;
    private Animator animator;
    public AudioClip slam, star;
    private AudioSource audioSource;
    
    void Start()
    {
      Br = GetComponent<BeamRain>();
      animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
     
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
            
           
            BeamAttack();
            DoneAttacking = false;
        }
        if(randomChoice == 30)
        {
           
            
            WavePattern(); 
            DoneAttacking = false;
        }
       
    }

    void RainBeam()
    {
        animator.Play("Slam");
        audioSource.PlayOneShot(slam);
        Br.RainAttack();
        StartCoroutine(Cooldown());
    }

    void WavePattern()
    {
        animator.Play("Rain");
        audioSource.PlayOneShot(star);
        Wave.enabled = true;
        Wave.TidalWave();
        StartCoroutine(Cooldown());
    }

    void BeamAttack()
    {
        
        animator.Play("Beam");
        beam.ShootLaser();
        StartCoroutine(Cooldown());
    }

   



    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(timer);
        DoneAttacking = true;
        Wave.enabled = false;
    }
}
