using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManagerForGame : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject MyltiplieButton;
    MyCanvas myCanvas;
    private void Start()
    {
        myCanvas = GameObject.Find("Canvas").GetComponent<MyCanvas>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3891025",false);
        }


    }
    private void Update()
    {
        if (Advertisement.IsReady())
        {
            MyltiplieButton.SetActive(true);
        }
        else MyltiplieButton.SetActive(false);
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            myCanvas.MultiplieScore();
            gameManager.SaveGameData();
        }
        else 
        { 
            //text adds is not ready
        }
        Debug.Log("ADVERROE");
    }
    public void ShowAdForDeath()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
            gameManager.SaveGameData();
        }
        else
        {
            //text adds is not ready
        }
        Debug.Log("ADVERROE");
    }




}
