using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainMenu : MonoBehaviour // передвижение камеры
{
    public Transform[] target;
    public Vector3 offset;
    public float smoothspeed = 0.125f;
    public bool inShop = false;
    public int index = 0;
    public Vector3 startPos;
    public Quaternion startRot;
    private MainMenuManager priceText;
    private MainMenuManager medalText;
    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        priceText = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
        medalText = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        NavigationOnMainMenu();
        SetPrice();

    }
    public void NavigationOnMainMenu()
    {
        if (!inShop)
        {
            transform.position = Vector3.Lerp(transform.position, startPos,smoothspeed) ;
            transform.rotation = startRot;
        }
        else if (inShop == true)
        {
            Vector3 desiredPosition = target[index].position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
            transform.position = smoothedPosition;

            transform.LookAt(target[index]);
        }
    }


    public void SetPrice()
    {
        if (inShop)
        {
            switch (index)
            {

                case 0://сюда поставить условие если не куплена?
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 1:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 2:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 3:
                    priceText.priceTextMedal.SetText(priceText.currentCostMedals.ToString());
                    break;
                case 4:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 5:
                    priceText.priceTextMedal.SetText(priceText.currentCostMedals.ToString());
                    break;
                case 6:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 7:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 8:
                    priceText.priceTextMedal.SetText(priceText.currentCostMedals.ToString());
                    break;
                case 9:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 10:
                    priceText.priceTextMedal.SetText(priceText.currentCostMedals.ToString());
                    break;
                case 11:
                    priceText.priceText.SetText(priceText.currentCost.ToString());
                    break;
                case 12:
                    priceText.priceTextMedal.SetText(priceText.currentCostMedals.ToString());
                    break;
                default:
                    priceText.priceText.SetText("");
                    break;
            }
        }
    }
    
}
