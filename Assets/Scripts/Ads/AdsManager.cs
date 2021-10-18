using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private MainMenuManager mainMenuManager;
    private GameManager gameManager;
    private void Start()
    {
        mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3891025",false);
        }
    }
    
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            mainMenuManager.AddMedal();
            gameManager.SaveGameData();
        }
    }
    
}
