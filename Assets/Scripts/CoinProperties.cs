using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinProperties : MonoBehaviour
{
    [SerializeField] float speedrotation = 10f;
    public int score;
    private MyCanvas canvasRef;
    public AudioSource coinPickUp;
    public AudioClip[] soundOfPickUp;
    private int indexOfSounds;
    private int multiplier;
    void Start()
    {
        coinPickUp = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        canvasRef = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        multiplier  = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControls>().scoreMultiplier;
        score = score * multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up,speedrotation*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            canvasRef.SetScoreText(score);
            indexOfSounds = Random.Range(0, soundOfPickUp.Length);
            coinPickUp.PlayOneShot(soundOfPickUp[indexOfSounds],1);
            Destroy(gameObject);
            
        }
    }

    

}
