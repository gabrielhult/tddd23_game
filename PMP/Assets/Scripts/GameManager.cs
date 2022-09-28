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
    public int levelCount;
    public bool changePlayerAngleAndDir;
    public bool isGameOver;
    public bool isPaused;
    public bool isClimbable;
    public bool roundStarted;
    
    public GameObject climbObject;
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;
    public LevelLoader levelLoader;

    public float gameplayScaleMultiplier;
    public float gameplayScaleAdder;
    public float gameplayScaleTimer;
    
    public TextMeshProUGUI bananaEndScore;
    public TextMeshProUGUI distanceEndScore;

    private void Awake() {
        Instance = this;
        isGameOver = false;
        roundStarted = false;
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
                LoadGameScene("GameScene");
            }else if(Input.GetKeyDown(KeyCode.Q)){
                Quit();
            }else if(Input.GetKeyDown(KeyCode.H)){
                LoadGameScene("HighScoreScene");
            }
        }else if(isPaused){
            if(Input.GetKeyDown(KeyCode.R)){ //Retry
                changePauseState();
                Resume();
            }else if(Input.GetKeyDown(KeyCode.Q)){
                Quit();
            }else if(Input.GetKeyDown(KeyCode.H)){
                changePauseState();
                Resume();
                LoadGameScene("HighScoreScene");
            }
        }
    }

    
    public void NextLevel(GameObject gameObject){
        changePlayerAngleAndDir = true;
        levelCount++;
        gameObject.SetActive(false);
    }

    public void CollectBanana(GameObject gameObject){
        if(playerInventory != null){
            playerInventory.ScoreCollected();
            if(playerInventory.ScoreCounter % 10 == 0){
                AudioManager.Instance.PlaySound("TenBananasCollected");
            }else{
                AudioManager.Instance.PlaySound("BananaCollect");
            }
            
            gameObject.SetActive(false);
        }
    }

    public void GameOver(){
        //AudioManager.Instance.PlaySound("BongeLost");
        bananaEndScore.text = playerInventory.ScoreCounter.ToString();
        distanceEndScore.text = playerInventory.DistanceCounter.ToString();
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
