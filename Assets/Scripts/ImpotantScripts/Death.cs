using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    MyCanvas canvasRef ;
    public int deathCount;
    private AdsManagerForGame adsForGame;


    private void Start()
    {
        deathCount = PlayerPrefs.GetInt("dc", deathCount);
        PlayerPrefs.GetInt("dc", deathCount);
        // PlayerPrefs.SetInt("DeathCount", deathCount);
        canvasRef = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        adsForGame = GameObject.Find("AdsManager").GetComponent<AdsManagerForGame>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasRef.isAlive = false;
            deathCount++;
            PlayerPrefs.SetInt("dc", deathCount);
            
           // Debug.Log("GameOver");

        }
    }
    public void JustDeath()
    {
        canvasRef.isAlive = false;
    }
    private void Update()
    {
       
        if(PlayerPrefs.GetInt("dc", deathCount) >= 5)
        {
            PlayerPrefs.SetInt("dc", 0);

            adsForGame.ShowAdForDeath();
        }
    }
}
