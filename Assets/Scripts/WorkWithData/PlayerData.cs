using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
    public int medals;
    public int cash;
    public int currentCar;
    public int[] savePurshareCars;

    // переменная  с машинами 

    //constructor
    public PlayerData (GameManager manager)
    {
        cash = manager.playerCash;
        medals = manager.playerMedals;
        currentCar = manager.currentCar;
        savePurshareCars = manager.purshare;
        for (int i = 0; i < manager.purshare.Length; i++)//проверка массива типа 0 0 0 1 0 0 0 0 // работает коорректно
        {
           
            Debug.Log(savePurshareCars[i]);
        }
        // написать как именно сохранять переменные
        // переменная для машин 

    }
}
