using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MyCanvas : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI levelText;
    private int levelnum=0;
    public bool levelmustChange = true;

    //Text
    public TextMeshProUGUI speedometer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI recordScoreText;
    
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI youLose;
    public TextMeshProUGUI DistanceText;

    //Panel
    public GameObject playerStatsPanel;
    public GameObject EndLevelPanel;
    public GameObject pausePanel;
    public GameObject MultiplieScoreButton;
    public GameObject LeftTipPanel;
    public GameObject RightTipPanel;

    [HideInInspector] public float speed;
    //ScoreSystem
    [HideInInspector] public int money;
    [HideInInspector] public int medals;
    [HideInInspector] public int currentMoney ;
    [HideInInspector] public int record;
    //TimeSystem
    [HideInInspector] public float time= 120;
    [HideInInspector] public float currentTime = 0;
    
    //StatsSystem
    [HideInInspector] public int health = 100;
    [HideInInspector] public float MaxNitro = 200;
    [HideInInspector] public float CurrentNitro = 0;
    //
    [HideInInspector] public double pathLehgth = 0;
    public bool isAlive = true;
    public bool gameIsLoad = false;
    //brake system
    public bool turnLeft = false;
    public bool turnRight = false;
    public bool noTurn = true;
    public bool brake = false;
    public float brakeCooldown = 5.0f;
    public float brakeTime;
    public bool canBrake = true;
    public bool startBreake ;
    public bool endBreake;
    public float cooldown = 0;
    //добавить индивидуальные значения для тормоза
    private float carBreakTime;
    public Slider sliderBrake;
    public Image fillForBrake;

    //References
    Rigidbody rb;
    private float currentDrag;
    StartGame startGame;
    Transform player;
    public Transform startPosition;
    GameManager gameManager;
    public int[] purshare;
    public int numOfAds = 0;
    private void Awake()
    {
        Time.timeScale = 1f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LoadPlayerProgress();
        currentMoney = 0;
        currentDrag = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControls>().standartDrag;
        carBreakTime = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControls>().carBreakTime;
        brakeTime = carBreakTime;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Time
        timeText.SetText("Time: " + time);
        sliderBrake.maxValue = brakeTime;///////////////
        recordScoreText.SetText("Record :"+ PlayerPrefs.GetInt("Record",record).ToString());
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        startGame = GameObject.Find("StartGameTrig").GetComponent<StartGame>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        //METHODS
        SetScoreText(0);
        SetDistanceText();
        anim.SetTrigger("LevelChange");

    }

    
    void LateUpdate()
    {
        BrakeSystem();
        cooldown += Time.deltaTime;
        if (startGame.gameIsStart == true)
        {
            
            SetTimeText();
            SetDistanceText();
                LeftTipPanel.SetActive(false);
                RightTipPanel.SetActive(false);
        }
        PanelController();
        //SetSpeedText();

        //Debug.Log(currentMoney % 30);

    }
    private void BrakeSystem()
    {
        sliderBrake.value = brakeTime;
        if (startBreake == true && endBreake != true)//начал тормозить
        {
            brakeTime -= Time.deltaTime;
            brake = true;
            rb.drag = 0.5f;///
            //Debug.Log(brakeTime);
            if (brakeTime < 0)//долго тормозил и перегрел
            {
                brake = false;
                rb.drag = currentDrag;
                brakeCooldown = 0;
                canBrake = false;
                startBreake = false;
                endBreake = true;
                //Debug.Log("PEREGREV");
            }
            
        }
        else if (brakeTime > 0 && endBreake == true)//не перегрел
        {
            brake = false;
            rb.drag = currentDrag;
            brakeTime += Time.deltaTime;

            if (brakeTime > carBreakTime)// esli bolishe 2 
            {
                brakeTime = carBreakTime;
                canBrake = true;
                //Debug.Log("NEPOLNAY BRAKE");
            }
        }
        else if (startBreake == false && endBreake == true)//закончил тормозить
        {
            brakeCooldown += Time.deltaTime;
            brake = false;
            rb.drag = currentDrag;
            
            if (brakeCooldown > 5)//ждешь кулдаун
            {
                brakeCooldown = 5;
               // Debug.Log("COOOLDOOOOWN");
                canBrake = true;
            }
            if (canBrake)
            {
                brake = false;
                rb.drag = currentDrag;
                brakeTime += Time.deltaTime;
                if (brakeTime > carBreakTime)
                {
                    brakeTime = carBreakTime;
                }
            }

        }
    }
    /*private void SetSpeedText()//отвечает за показ скорости
    {
        speed = Mathf.RoundToInt(rb.velocity.magnitude * 3.6f);
        speedometer.SetText( " "+speed +" Km/h" );
    }*/
    private void SetDistanceText()
    {
        //pathLehgth = Mathf.RoundToInt((player.transform.position.z - startPosition.position.z));
        pathLehgth += Mathf.RoundToInt((rb.velocity.magnitude * 3.6f)*Time.deltaTime);
        DistanceText.SetText( "Distance: "+pathLehgth + " m");
    }

    public void SetScoreText(int scoretoAdd)//вызывается когда нужно добавить очки к общему счету
    {
       currentMoney += scoretoAdd;
       scoreText.SetText("Money: " + currentMoney);
    }
    public void ADDCASHFORTEST(int i)
    {
        SetScoreText(i);
    }
    public void SetTimeText()
    {
        currentTime += Time.deltaTime;
        time = Mathf.RoundToInt(currentTime);
        timeText.SetText("Time: " + time);

        if (isAlive && startGame.gameIsStart)
        {
            
            //Debug.Log("NOW");
            if(levelmustChange && currentMoney%100 == 0)
            {
                ShowLevelText();
                currentDrag -= 0.02f;
                rb.drag = currentDrag;
                levelmustChange = false;
                
            }
            if (levelmustChange!=true&&currentMoney % 100 != 0)
            {
               // Debug.Log("NOW LEVEL CHANGE");
                levelmustChange = true;
            }


        }
        
    }// показывает сколько времени прошло

    private void ShowLevelText()
    {
        levelnum++;
        levelText.SetText("Level " + levelnum);
        anim.SetTrigger("LevelChange");
 
    }
    public void TurnLeft()
    {

        turnLeft = true;
        noTurn = false;
    }
    public void TurnRight()
    {
        turnRight = true;
        noTurn = false;
    }
    public void NoTurn()
    {
        turnRight = false;
        turnLeft = false;
        noTurn = true;
    }
    public void Brake()
    {
        if (turnLeft && turnRight && rb.velocity.magnitude * 3.6f >= 10 ) 
        {
            if (canBrake && cooldown>2)
            {
                cooldown = 0;
                startBreake = true;
                endBreake = false;
                //Debug.Log("START BRAKE");

            }
                turnRight = false;
                turnLeft = false;
        }
    }
     public void BreakButton()
    {
        if (canBrake && cooldown > 1)
        {
            cooldown = 0;
            startBreake = true;
            endBreake = false;
            Debug.Log("START BRAKE");
        }
    }
    public void outFromBreake()
    {
        endBreake = true;
        startBreake = false;
        brake = false;
        rb.drag = currentDrag;
        //Debug.Log("END BRAKE");
        //вписать функционал выхода из состояния тормоза
    }
    public void OutBrake()//yprostit
    {
        if( turnLeft || turnRight || turnLeft ==false && turnRight == false)
        {
            endBreake = true;
            startBreake = false;
            brake = false;
            rb.drag = currentDrag;
            if (turnLeft == true)
            {
                TurnLeft();
            }
            else if (turnRight == true)
            {
                TurnRight();
            }
            else if (turnRight == false && turnLeft == false)
            {
                NoTurn();
            }
        }
    }
     public void PausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void continueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PanelController()//переключение панелей, Выставление личного рекорда
    {
        if (isAlive == false)
        {
            playerStatsPanel.SetActive(false);
            EndLevelPanel.SetActive(true);
            
            if ( numOfAds<1 && currentMoney != 0)
            {
                MultiplieScoreButton.gameObject.SetActive(true);
            }
            else MultiplieScoreButton.gameObject.SetActive(false);
            
            endScoreText.SetText("Money:" + currentMoney);
            //Player Personal Record
            if (currentMoney > PlayerPrefs.GetInt(("Record"),0))
            {
                record = currentMoney;
                recordScoreText.SetText("!New record! :" + record);
                PlayerPrefs.SetInt("Record", record);
            }
        }
    }
    public void RestartLevel()// при вызове перезагружает уровень
    {
        money += currentMoney;
        gameManager.playerMedals = medals;
        gameManager.playerCash = gameManager.playerCash + money;
        gameManager.purshare = purshare;
        gameManager.SaveGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()// при вызове перенаправляет в главное меню
    {
        money += currentMoney;
        gameManager.playerMedals = medals;
        gameManager.playerCash += money;
        gameManager.purshare = purshare;
        gameManager.SaveGameData();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);


    }
    public void MultiplieScore()
    {
        currentMoney += currentMoney;
        scoreText.SetText("Money: " + currentMoney);
        MultiplieScoreButton.gameObject.SetActive(false);
        numOfAds++;

    }
    void LoadPlayerProgress()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        //Debug.Log(data.cash);
        money = data.cash;
        medals = data.medals;
        purshare = data.savePurshareCars;
        gameManager.currentCar = data.currentCar;

    }
    

   

}
