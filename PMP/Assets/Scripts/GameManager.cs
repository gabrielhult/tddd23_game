using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Borde göra att jag kan ta den varifrån somhelst
    public CameraBehaviour cameraBehaviour;
    public PlayerInventory playerInventory;
    public StartGameUI startGameUI;
    [HideInInspector]
    public bool isGameOver;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public bool isClimbable;
    [HideInInspector]
    public bool roundStarted;
    [HideInInspector]
    public bool cancelClimbing;
    
    public GameObject climbObject;
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;
    public LevelLoader levelLoader;
    [HideInInspector]
    public GameObject[] activeStaticObstacles;
    [HideInInspector]
    public GameObject[] activeSidewaysObstacles;
    [HideInInspector]
    public GameObject[] activeUpAndDownObstacles;

    [HideInInspector]
    public float gameplayScaleMultiplier;
    public float gameplayScaleAdder;
    public float gameplayScaleTimer;
    public float basePowerUpDuration;
    public bool closePowerUp;
    public bool isPowerUp; //Helps us decide whether or not to play.

    //Power up related variables
    public string[] powerUpArray;
    [HideInInspector]
    public string chosenPowerUp;
    public bool distanceBonusLimiter;
    [HideInInspector]
    public bool increaseDistanceTimerDisabled;
    public int bananasForPowerUp;
    
    public TextMeshProUGUI bananaEndScore;
    public TextMeshProUGUI distanceEndScore;

    private void Awake() {
        Instance = this;
        isGameOver = false;
        roundStarted = false;
        distanceBonusLimiter = true;
        cancelClimbing = false;
        increaseDistanceTimerDisabled = false; //Makes sure timer doesn't get activated if distance power-up is used.
        gameplayScaleMultiplier = 1f;
        StartCoroutine(ScaleGameplay());
    }

    void Start(){
        cameraBehaviour = cameraBehaviour.GetComponent<CameraBehaviour>();
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        startGameUI = startGameUI.GetComponent<StartGameUI>();
        levelLoader = levelLoader.GetComponent<LevelLoader>();

    }

    void Update(){ //Kan göras om enligt https://www.youtube.com/watch?v=Hn804Wgr3KE vid behov

        if(playerInventory != null){
            playerInventory.AwardDistanceScore(); 
        }


        if(isGameOver){
            //Check if new highscore is set for banana and/or distance
            playerInventory.BananaHighScoreCheck();  
            playerInventory.DistanceHighScoreCheck();


            if(Input.GetKeyDown(KeyCode.R)){ //Retry
                AudioManager.Instance.PlaySound("ButtonClick");
                LoadGameScene("GameScene");
            }else if(Input.GetKeyDown(KeyCode.Q)){
                Quit();
            }else if(Input.GetKeyDown(KeyCode.H)){
                AudioManager.Instance.PlaySound("ButtonClick");
                LoadGameScene("HighScoreScene");
            }
        }else if(isPaused){
            if(Input.GetKeyDown(KeyCode.R)){ //Retry
                AudioManager.Instance.PlaySound("ButtonClick");
                changePauseState();
                Resume();
            }else if(Input.GetKeyDown(KeyCode.Q)){
                Quit();
            }else if(Input.GetKeyDown(KeyCode.H)){
                AudioManager.Instance.PlaySound("ButtonClick");
                changePauseState();
                Resume();
                LoadGameScene("HighScoreScene");
            }
        }
    }


    public void CollectBanana(GameObject gameObject){
        if(playerInventory != null){
            playerInventory.ScoreCollected();

            if(playerInventory.ScoreCounter % bananasForPowerUp == 0){
                closePowerUp = false;
                Debug.Log(chosenPowerUp);
                AudioManager.Instance.PlaySound("TenBananasCollected");
                if(chosenPowerUp == ""){
                    Debug.Log("Power-up time but no power-up set! Fix this if I see it.");
                }
                StartCoroutine(awardPowerUp(chosenPowerUp));
            }else{
                //If we are one away from a power-up
                if(playerInventory.ScoreCounter % bananasForPowerUp == bananasForPowerUp - 1){
                    chosenPowerUp = powerUpArray[Random.Range(0, powerUpArray.Length)]; //Choose power-up and display it over the banana
                    closePowerUp = true;
                }
                AudioManager.Instance.PlaySound("BananaCollect");
            }
            
            gameObject.SetActive(false);
        }
    }

    public void playButtonClick(){
        AudioManager.Instance.PlaySound("ButtonClick");
    }

    public void GameOver(){
        AudioManager.Instance.PlaySound("GameOver");
        if(!isGameOver){
            AudioManager.Instance.PlaySound("BongeLost");
        }
        isGameOver = true;  
        bananaEndScore.text = playerInventory.ScoreCounter.ToString();
        distanceEndScore.text = playerInventory.DistanceCounter.ToString();
    }

    public void Resume(){
        Time.timeScale = 1;
        GameResumed.Invoke();
        isPaused = false;
    }

    public void Pause(){
        Time.timeScale = 0;
        GamePaused.Invoke();
    }

    public void changePauseState(){
        isPaused = !isPaused;
    }

    public void LoadGameScene(string sceneName){
        levelLoader.LevelLoad(sceneName);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quit!");
    }

    public GameObject getClimbObject(){
        return climbObject;
    }

    public void setClimbObject(GameObject gameObject){
        climbObject = gameObject;
    }


    IEnumerator awardPowerUp(string chosenPowerUp){
        isPowerUp = true;
        //Detta kan verkligen se bättre ut, men kan inte ha detta i extern funktion (funkar ej då)
        if(chosenPowerUp == "NoObstacles"){
            cancelClimbing = true;
            activeStaticObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obst in activeStaticObstacles){
                obst.SetActive(false);
            }
            activeSidewaysObstacles = GameObject.FindGameObjectsWithTag("Sideways");
            foreach(GameObject obst in activeSidewaysObstacles){
                obst.SetActive(false);
            }
            activeUpAndDownObstacles = GameObject.FindGameObjectsWithTag("UpAndDown");
            foreach(GameObject obst in activeUpAndDownObstacles){
                obst.SetActive(false);
            }
        }else if(chosenPowerUp == "IncreaseDistanceAward"){
            increaseDistanceTimerDisabled = true;
        }
        //wait x seconds
        yield return new WaitForSeconds(basePowerUpDuration * GameManager.Instance.gameplayScaleMultiplier);
        //Detta kan verkligen se bättre ut
        if(chosenPowerUp == "NoObstacles"){
            foreach(GameObject obst in activeStaticObstacles){
                obst.SetActive(true);
            }
            foreach(GameObject obst in activeSidewaysObstacles){
                    obst.SetActive(true);
            }
            foreach(GameObject obst in activeUpAndDownObstacles){
                obst.SetActive(true);
            }
            cancelClimbing = false;
        }
        
        //turn it off
        Debug.Log("Turn off");
        chosenPowerUp = "";
        increaseDistanceTimerDisabled = false;
        isPowerUp = false;
    }

    IEnumerator ScaleGameplay(){
        //Logic for how often new tiles spawn.

        yield return new WaitForSeconds(gameplayScaleTimer); //Optimera detta
        if(roundStarted){
            if(!GameManager.Instance.isGameOver){
                gameplayScaleMultiplier += gameplayScaleAdder;
            }
        }
        StartCoroutine(ScaleGameplay()); //Makes this IEnumerator loop.
    }
    

}
