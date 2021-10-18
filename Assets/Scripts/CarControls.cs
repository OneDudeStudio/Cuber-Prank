using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarControls : MonoBehaviour
{
    //References
     public Rigidbody rb;
    MyCanvas healthControl;
    MyCanvas nitroControl;

    //Variables
    public float standartDrag = 0.2f;
    public float carBreakTime;
    public int scoreMultiplier;

    //Particles
    public ParticleSystem nitroParticle;
    public ParticleSystem lightSmokeParticle;
    public ParticleSystem mediumSmokeParticle;
    public ParticleSystem fireSmokeParticle;
    public ParticleSystem explosionParticle;

    public ParticleSystem LeftTireSmokeParticle;
    public ParticleSystem RightTireSmokeParticle;

    
    private MyCanvas canvas;

    void Start()
    {
        
        healthControl = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        canvas = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        nitroControl = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        rb = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        
        Death();
        if (canvas.brake && rb.velocity.magnitude * 3.6f > 20/*joystick.Vertical < 0 && rb.velocity.magnitude * 3.6f > 20*/)
        {
            LeftTireSmokeParticle.Play();
            RightTireSmokeParticle.Play();
        }
        else
        {
            LeftTireSmokeParticle.Stop();
            RightTireSmokeParticle.Stop();
        }

        
    }
    void Death()
    {
        if(healthControl.isAlive == false)
        {
            explosionParticle.Play();
            rb.drag = 1f;
        }
        
    }

    





}
