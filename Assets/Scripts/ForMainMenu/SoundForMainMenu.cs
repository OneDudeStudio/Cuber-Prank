using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForMainMenu : MonoBehaviour
{
    
    public AudioClip buttonClick;
    public AudioClip changeCar;

    public AudioSource buttonS;
    public AudioSource changeS;

    public void button()
    {
        buttonS.volume = 0.9f;
        buttonS.PlayOneShot(buttonClick);

    }
    public void change()
    {
        changeS.volume = 0.9f;
        changeS.PlayOneShot(changeCar);
    }

}
