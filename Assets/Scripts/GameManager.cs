using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{   //деньги игрока
    public int playerCash;
    public int playerMedals;
    //машины игрока 
    public int[] purshare;
    public int alreadyByed;
    public bool isPurshared = false;

    public int currentCar; //номер выбранной машины

    public void SaveGameData()//готово
    {
        SaveSystem.SaveProgress(this);
        Debug.Log("GameSaved");
    }
    public void LoadGameData()//готово
    {
        PlayerData data = SaveSystem.LoadPlayer();
        playerCash = data.cash;
        playerMedals = data.medals;
        currentCar = data.currentCar;
        purshare = data.savePurshareCars;
        Debug.Log("GameLoaded");
    }
    
    
}
