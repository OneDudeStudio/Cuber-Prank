using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip motor;
    public AudioClip speed;
    public AudioClip skid;
    public AudioClip carTheme;
    private AudioSource motorS;
    private AudioSource speedS;
    private AudioSource skidS;
    private AudioSource themeS;
    private Rigidbody rb;
    public float musicVolume;
    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        motorS = gameObject.AddComponent<AudioSource>();
        motorS.clip = motor;
        motorS.loop = true;
        motorS.volume = 0.5f;
        motorS.pitch = 0.5f;
        motorS.Play();

        themeS = gameObject.AddComponent<AudioSource>();
        themeS.clip = carTheme;
        themeS.loop = true;
        themeS.volume = musicVolume;
        themeS.Play();

        speedS = gameObject.AddComponent<AudioSource>();
        speedS.clip = speed;
        speedS.loop = true;
        speedS.volume = 0.6f;
        speedS.pitch = 0.0f;
        speedS.Play();

        skidS = gameObject.AddComponent<AudioSource>();
        skidS.clip = skid;
        skidS.loop = true;
        skidS.volume = 0.0f;
        //skidS.pitch = 0.0f;
        skidS.Play();

    }
    
    // Update is called once per frame
    void Update()
    {
        motorS.pitch = (float)System.Math.Round(Mathf.Lerp(0.5f, 0.5f + Mathf.Abs((rb.velocity.magnitude*3.6f)/20) , Time.deltaTime * rb.velocity.magnitude * 2), 2);
        speedS.pitch = (float)System.Math.Round(Mathf.Lerp(0f, 1f + Mathf.Abs((rb.velocity.magnitude * 3.6f) / 20) , Time.deltaTime * rb.velocity.magnitude * 2), 2);
    }

    public void playSkid(float volume)
    {
        skidS.volume = Mathf.Abs(volume);
    }
}
