
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Button startGameButton;//кнопка в игру
    public Button ShopButton;//кнопка в магазин
    public GameObject PanelForAsyncLoad;//панель
    public GameObject loadingBar;//префаб загрузочного элемента
    public GameObject MainPanel;//панель
    public GameObject ExitPanel;
    public GameObject MedalSpawner;//панель
    public GameObject ShopPanel;//панель
    public GameObject StaticticPanel;//панель
    private CameraMainMenu mainCamera;
    private GameManager gameManager;

    public TextMeshProUGUI NotEnoughtMoney;//не хватает денег
    public TextMeshProUGUI alreadyPurshare;//уже куплена
    public TextMeshProUGUI coinsText;//сколько денег у игрока

    public TextMeshProUGUI medalText;//сколько medal у игрока
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI priceTextMedal;//сколько стоит машина
    public TextMeshProUGUI chooseOneText;
    public int currentCost;
    public int currentCostMedals;

    public int firstPlay=0;
    public bool forMedals;

    private CharacteristicsOfThisCar visualPart;

    private void Awake()
    {
        PlayerPrefs.GetInt("this is first play?", firstPlay);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMainMenu>();
        //Debug.Log(PlayerPrefs.GetInt("this is first play?", firstPlay));
        if (PlayerPrefs.GetInt("this is first play?", firstPlay) == 0) 
        {
            gameManager.SaveGameData();//добавить if
        }
        PlayerPrefs.SetInt("this is first play?", 1);
        gameManager.LoadGameData();
        //Debug.Log(PlayerPrefs.GetInt("this is first play?", firstPlay));
    }
    private void Start()
    {
        MainPanel.gameObject.SetActive(true);
        ShopPanel.gameObject.SetActive(false);
        PanelForAsyncLoad.gameObject.SetActive(false);
        coinsText.SetText(gameManager.playerCash + " $");
        medalText.SetText(gameManager.playerMedals + "");
        for (int i = 0; i < gameManager.purshare.Length; i++)
        {
            if (gameManager.purshare[i] == 1)
            {
                mainCamera.target[i].GetComponent<CharacteristicsOfThisCar>().isPurshared = true;
            }
            else
            {
                mainCamera.target[i].GetComponent<CharacteristicsOfThisCar>().isPurshared = false;
            }
        }


    }
    public void StartGame() // из главного меню в экран загрузки и из него в игру
    {
        PanelForAsyncLoad.SetActive(true);
        loadingBar.SetActive(true);
        coinsText.gameObject.SetActive(false);
        medalText.gameObject.SetActive(false);
        gameManager.SaveGameData();
        Time.timeScale = 1f;
    }

    public void ToShop()// из главного меню в магазин
    {
        MainPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(true);
        StaticticPanel.gameObject.SetActive(true);
        mainCamera.inShop = true;
        mainCamera.index = 0;//////////////////////!!!!!!!!!!!!!!!!!!!!!
        forMedals =false;
        visualPart = GameObject.Find(mainCamera.index.ToString()).GetComponent<CharacteristicsOfThisCar>();
        visualPart.VisualUpdateStatistic();
        gameManager.alreadyByed = gameManager.purshare[mainCamera.index];
        //Debug.Log("Car Number is " + mainCamera.index + " I went from MainMenu");
        gameManager.isPurshared = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared;
        UpdateIndex();
        if (gameManager.isPurshared|| gameManager.alreadyByed == 1)
        {
            priceText.gameObject.SetActive(false);
            priceTextMedal.gameObject.SetActive(false);
            alreadyPurshare.gameObject.SetActive(true);
            chooseOneText.gameObject.SetActive(true);
        }
        else
        {
            priceText.gameObject.SetActive(true);
            priceTextMedal.gameObject.SetActive(true);
            alreadyPurshare.gameObject.SetActive(false);
            chooseOneText.gameObject.SetActive(false);
        }
        



    }

    public void buyCar()// берет значение стоимости из префаба, сравнивает с деньгами игрока и покупает если достаточкно
    {
        if (forMedals == false)// za denigi
        {
            if (mainCamera.inShop && gameManager.playerCash >= currentCost && gameManager.alreadyByed == 0)
            {
                gameManager.playerCash -= currentCost;
                coinsText.SetText(gameManager.playerCash + " $");
                mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared = true;
                gameManager.isPurshared = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared;
                priceText.gameObject.SetActive(false);
                chooseOneText.gameObject.SetActive(true);
                alreadyPurshare.gameObject.SetActive(true);
                NotEnoughtMoney.gameObject.SetActive(false);
                //Debug.Log("I purshared a car " + mainCamera.index + " " + gameManager.isPurshared);
                if (mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared)
                {
                    gameManager.purshare[mainCamera.index] = 1;
                }
                SaveSystem.SaveProgress(gameManager);//сохранение происходит тут
            }
            else if (gameManager.alreadyByed == 1)
            {
                alreadyPurshare.gameObject.SetActive(true);
                chooseOneText.gameObject.SetActive(true);
                //NotEnoughtMoney.gameObject.SetActive(true);
                //Debug.Log("Already purshared");
                //Debug.Log("if 1 good "+purshare[mainCamera.index]);
            }
            else
            {
                NotEnoughtMoney.gameObject.SetActive(true);
                NotEnoughtMoney.SetText("Not Enought Money");
               // Debug.Log("if 0 good " + gameManager.purshare[mainCamera.index]);
                //Debug.Log("Not enought money");
            }

        }
        else if(forMedals == true) // zamedali
        {
            if (mainCamera.inShop && gameManager.playerMedals >= currentCostMedals && gameManager.alreadyByed == 0)
            {
                gameManager.playerMedals -= currentCostMedals;
                medalText.SetText(gameManager.playerMedals + "");
                mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared = true;
                gameManager.isPurshared = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared;
                priceTextMedal.gameObject.SetActive(false);//medal text
                chooseOneText.gameObject.SetActive(true);
                alreadyPurshare.gameObject.SetActive(true);
                NotEnoughtMoney.gameObject.SetActive(false);
                //Debug.Log("I purshared a car " + mainCamera.index + " " + gameManager.isPurshared);
                if (mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared)
                {
                    gameManager.purshare[mainCamera.index] = 1;
                }
                SaveSystem.SaveProgress(gameManager);//сохранение происходит тут

            }

            else if (gameManager.alreadyByed == 1)
            {
                alreadyPurshare.gameObject.SetActive(true);
                chooseOneText.gameObject.SetActive(true);
                //NotEnoughtMoney.gameObject.SetActive(true);
                //Debug.Log("Already purshared");
                //Debug.Log("if 1 good "+purshare[mainCamera.index]);
            }
            else
            {
                NotEnoughtMoney.SetText("Not Enought Medals");
                NotEnoughtMoney.gameObject.SetActive(true);
                MedalSpawner.gameObject.SetActive(true);
                //Debug.Log("if 0 good " + gameManager.purshare[mainCamera.index]);
                //Debug.Log("Not enought money");
            }
        }

         


    }
    public void BackToMainMenu()// из магазина в главное меню
    {
        MainPanel.gameObject.SetActive(true);
        ShopPanel.gameObject.SetActive(false);
        StaticticPanel.gameObject.SetActive(false);
        mainCamera.inShop = false;
        gameManager.SaveGameData();
        NotEnoughtMoney.gameObject.SetActive(false);
        alreadyPurshare.gameObject.SetActive(false); 
        //!!!!!!!!!!!!!!!
        //coinsText.SetText(playerCash + " $");

    }
    public void PreviousCar()
    {
        if (mainCamera.index == 0)
        {
            return;
        }
        else
        {
            mainCamera.index--;
            UpdateIndex();
            visualPart = GameObject.Find(mainCamera.index.ToString()).GetComponent<CharacteristicsOfThisCar>();
            visualPart.VisualUpdateStatistic();
            NotEnoughtMoney.gameObject.SetActive(false);
            MedalSpawner.gameObject.SetActive(false);

            if (gameManager.alreadyByed == 1)
            {
                priceText.gameObject.SetActive(false);
                priceTextMedal.gameObject.SetActive(false);
                alreadyPurshare.gameObject.SetActive(true);
                chooseOneText.gameObject.SetActive(true);
            }
            else
            {
                if (forMedals)
                {
                    priceText.gameObject.SetActive(false);
                    priceTextMedal.gameObject.SetActive(true);
                }
                else
                {
                    priceText.gameObject.SetActive(true);
                    priceTextMedal.gameObject.SetActive(false);
                }
                alreadyPurshare.gameObject.SetActive(false);
                chooseOneText.gameObject.SetActive(false);
            }
        }

    }
    public void NextCar()
    {
        if (mainCamera.index == mainCamera.target.Length - 1)
        {
            //Debug.Log("max");
            return;
        }
        else
        {
            mainCamera.index++;
            UpdateIndex();
            visualPart = GameObject.Find(mainCamera.index.ToString()).GetComponent<CharacteristicsOfThisCar>();
            visualPart.VisualUpdateStatistic();
            NotEnoughtMoney.gameObject.SetActive(false);
            MedalSpawner.gameObject.SetActive(false);
            if (gameManager.alreadyByed == 1)
            {
                priceText.gameObject.SetActive(false);
                priceTextMedal.gameObject.SetActive(false);
                alreadyPurshare.gameObject.SetActive(true);
                chooseOneText.gameObject.SetActive(true);
            }
            else
            {
                if (forMedals)
                {
                    priceText.gameObject.SetActive(false);
                    priceTextMedal.gameObject.SetActive(true);
                }
                else
                {
                    priceText.gameObject.SetActive(true);
                    priceTextMedal.gameObject.SetActive(false);
                }
                alreadyPurshare.gameObject.SetActive(false);
                chooseOneText.gameObject.SetActive(false);
            }

        }

    }
    public void UpdateIndex()//обновление индекса и других переменных
    {
        forMedals = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().forMedals;
        if (mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().forMedals == false)
        {
            currentCost = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().theCost;
            currentCostMedals = 0;
        }
        else if ( mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().forMedals == true)
        {
            currentCostMedals = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().theCostMedals;
            currentCost = 0;
        }
        gameManager.isPurshared = mainCamera.target[mainCamera.index].GetComponent<CharacteristicsOfThisCar>().isPurshared; // пока работает только один раз, нужно добавить обновление состояния куплена или нет
        //Debug.Log(currentCost);
        gameManager.alreadyByed = gameManager.purshare[mainCamera.index];
        //Debug.Log(alreadyByed);
       // Debug.Log("Car number " + mainCamera.index + " was purshared? " + gameManager.isPurshared);
    }
    
        
    public void ChooseCurrentCar()
    {
        gameManager.currentCar = mainCamera.index;
        //Debug.Log("I choosed a car with number " + gameManager.currentCar);
    }
    public void AddCash()//поменять для игрока
    {
        gameManager.playerCash += 100;
        coinsText.SetText(gameManager.playerCash + " $");

    }
    public void AddMedal()
    {
        gameManager.playerMedals++;
        gameManager.playerMedals++;
        medalText.SetText(gameManager.playerMedals + "");
        MedalSpawner.SetActive(false);

    }
    public void ActivateExitPanel()
    {
        ExitPanel.SetActive(true);
    }
    public void BackToMainPanel()
    {
        ExitPanel.SetActive(false);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }





}
