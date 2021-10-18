using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarForPlay : MonoBehaviour
{
    public GameObject[] cars;
    int index;

    private void Awake()
    {
        LoadGame();
        var rotation = Quaternion.Euler(0, -90, 0);
        Instantiate(cars[index], transform.position, rotation);
    }
     void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        index = data.currentCar;
       // Debug.Log(index);
    }

}
