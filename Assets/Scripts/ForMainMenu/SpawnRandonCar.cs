using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandonCar : MonoBehaviour
{
    public GameObject[] cars;
    private GameManager gameManager;
    int index;
    GameObject currentCar;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        index = gameManager.currentCar;
        currentCar = Instantiate(cars[index], transform.position, cars[index].transform.rotation);
        
    }
    public void SpawnCar()
    {
        
        Destroy(currentCar);
        index = gameManager.currentCar;
        Debug.Log(index);
        currentCar = Instantiate(cars[index], transform.position, cars[index].transform.rotation);
    }

}
