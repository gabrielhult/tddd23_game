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
    public EndScore endScore;
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

    private Component[] childRendererObjects;

    [HideInInspector]
    public float gameplayScaleMultiplier;
    public float gameplayScaleAdder;
    public float gameplayScaleTimer;
    public float basePowerUpDuration;
    public float extraDoubleBananaScoreDuration;
    public float extraFeatherJumpDuration;
    public float extraClimbSpeedDuration;
    [HideInInspector]
    public float extraPowerUpDuration;
    public float obstacleOpacity;

    public bool isDefault;
    public bool isMagma;
    public bool isSwamp;
    public bool isArctic;

    //Power up related variables
    public string[] powerUpArray;
    [HideInInspector]
    public string chosenPowerUp;
    public bool distanceBonusLimiter;
    [HideInInspector]
    public bool increaseDistanceTimerDisabled;
    public int bananasForPowerUp;
    public bool closePowerUp;
    public bool isPowerUp; //Helps us decide whether or not to play.
    
    public TextMeshProUGUI bananaEndScore;
    public TextMeshProUGUI distanceEndScore;

    private float tempObstacleMaterial;

    private void Awake() {
        Instance = this;
        isGameOver = false;
        roundStarted = false;
        distanceBonusLimiter = true;
        cancelClimbing = false;
        isSwamp = false;
        isArctic = false;
        extraPowerUpDuration = 0;
        increaseDistanceTimerDisabled = false; //Makes sure timer doesn't get activated if distance power-up is used.
        gameplayScaleMultiplier = 1f;
        StartCoroutine(ScaleGameplay());
    }

    void Start(){
        cameraBehaviour = cameraBehaviour.GetComponent<CameraBehaviour>();
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        startGameUI = startGameUI.GetComponent<StartGameUI>();
        levelLoader = levelLoader.GetComponent<LevelLoader>();
        endScore = endScore.GetComponent<EndScore>();
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
            if(Input.GetKeyDown(KeyCode.R)){ //Resume
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
                AudioManager.Instance.PlaySound("TenBananasCollected");
                StartCoroutine(awardPowerUp(chosenPowerUp));
            }else{
                if(playerInventory.ScoreCounter % bananasForPowerUp == bananasForPowerUp - 1){ //If we are one away from a power-up
                    //TEST THIS
                    /* if(isPowerUp && chosenPowerUp != ""){ //Does this work?
                        chosenPowerUp = chosenPowerUp; //Keep the same power-up if we reach new power-up before on-going one hasn't expired
                    }else */ chosenPowerUp = powerUpArray[Random.Range(0, powerUpArray.Length)]; //Choose power-up and display it over the banana
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
        if(!isGameOver){
            AudioManager.Instance.PlaySound("GameOver");
            AudioManager.Instance.PlaySound("BongeLost");
            endScore.CountEndScore();
        }
        isGameOver = true;  
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
        if(chosenPowerUp == "NoObstacles"){
            cancelClimbing = true;
        }else if(chosenPowerUp == "IncreaseDistanceAward"){
            increaseDistanceTimerDisabled = true;
        }else if(chosenPowerUp == "DoubleBananaScoreCollect"){
            extraPowerUpDuration = extraDoubleBananaScoreDuration;
        }else if(chosenPowerUp == "IncreaseClimbSpeed"){
            extraPowerUpDuration = extraClimbSpeedDuration;
        }else if(chosenPowerUp == "FeatherJump"){
            extraPowerUpDuration = extraFeatherJumpDuration;
        }
        //wait x seconds
        yield return new WaitForSeconds(basePowerUpDuration * GameManager.Instance.gameplayScaleMultiplier + extraPowerUpDuration); 
        //Detta kan verkligen se bättre ut
        if(chosenPowerUp == "NoObstacles"){
            cancelClimbing = false;
        }
        
        //turn it off
        chosenPowerUp = "";
        extraPowerUpDuration = 0;
        increaseDistanceTimerDisabled = false;
        isPowerUp = false;
    }

    IEnumerator ScaleGameplay(){
        //Logic for how the gameplay scales as time goes

        yield return new WaitForSeconds(gameplayScaleTimer); //Optimera detta
        if(roundStarted){
            if(!GameManager.Instance.isGameOver){
                gameplayScaleMultiplier += gameplayScaleAdder;
            }
        }
        StartCoroutine(ScaleGameplay()); //Makes this IEnumerator loop.
    }
    

}
