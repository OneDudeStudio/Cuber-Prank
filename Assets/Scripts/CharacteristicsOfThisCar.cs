using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsOfThisCar : MonoBehaviour
{
    public int theCost = 0;
    public int theCostMedals = 0;
    public int state = 0;
    public bool isPurshared ;
    public bool forMedals;
    //for visual part
    public int maxspd;
    public int grid;
    public int brake;
    public int pointMultiplier;
    //visual part
    public Image spd1;
    public Image spd2;
    public Image spd3;
    public Image grid1;
    public Image grid2;
    public Image grid3;
    public Image brake1;
    public Image brake2;
    public Image brake3;
    public Image pointMultiplier1;
    public Image pointMultiplier2;
    public Image pointMultiplier3;



    public void VisualUpdateStatistic()
    {
        switch (maxspd)
        {
            case 1:
                spd1.gameObject.SetActive(true);
                spd2.gameObject.SetActive(false);
                spd3.gameObject.SetActive(false);
                break;
            case 2:
                spd1.gameObject.SetActive(true);
                spd2.gameObject.SetActive(true);
                spd3.gameObject.SetActive(false);
                break;
            case 3:
                spd1.gameObject.SetActive(true);
                spd2.gameObject.SetActive(true);
                spd3.gameObject.SetActive(true);
                break;
            default:
                spd1.gameObject.SetActive(false);
                spd2.gameObject.SetActive(false);
                spd3.gameObject.SetActive(false);
                break;
        }
        switch (grid)
        {
            case 1:
                grid1.gameObject.SetActive(true);
                grid2.gameObject.SetActive(false);
                grid3.gameObject.SetActive(false);
                break;
            case 2:
                grid1.gameObject.SetActive(true);
                grid2.gameObject.SetActive(true);
                grid3.gameObject.SetActive(false);
                break;
            case 3:
                grid1.gameObject.SetActive(true);
                grid2.gameObject.SetActive(true);
                grid3.gameObject.SetActive(true);
                break;
            default:
                grid1.gameObject.SetActive(false);
                grid2.gameObject.SetActive(false);
                grid3.gameObject.SetActive(false);
                break;
        }
        switch (pointMultiplier)
        {
            case 1:
                pointMultiplier1.gameObject.SetActive(true);
                pointMultiplier2.gameObject.SetActive(false);
                pointMultiplier3.gameObject.SetActive(false);
                break;
            case 2:
                pointMultiplier1.gameObject.SetActive(true);
                pointMultiplier2.gameObject.SetActive(true);
                pointMultiplier3.gameObject.SetActive(false);
                break;
            case 3:
                pointMultiplier1.gameObject.SetActive(true);
                pointMultiplier2.gameObject.SetActive(true);
                pointMultiplier3.gameObject.SetActive(true);
                break;
            default:
                pointMultiplier1.gameObject.SetActive(false);
                pointMultiplier2.gameObject.SetActive(false);
                pointMultiplier3.gameObject.SetActive(false);
                break;
        }
        switch (brake)
        {
            case 1:
                brake1.gameObject.SetActive(true);
                brake2.gameObject.SetActive(false);
                brake3.gameObject.SetActive(false);
                break;
            case 2:
                brake1.gameObject.SetActive(true);
                brake2.gameObject.SetActive(true);
                brake3.gameObject.SetActive(false);
                break;
            case 3:
               brake1.gameObject.SetActive(true);
               brake2.gameObject.SetActive(true);
               brake3.gameObject.SetActive(true);
                break;
            default:
                brake1.gameObject.SetActive(false);
                brake2.gameObject.SetActive(false);
                brake3.gameObject.SetActive(false);
                break;
        }

    }

}
