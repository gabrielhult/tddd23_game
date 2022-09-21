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
    

    private void Awake() {
        Instance = this;
        isGameOver = false;
        roundStarted = false;
    }

    void Start(){
        cameraBehaviour = cameraBehaviour.GetComponent<CameraBehaviour>();
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        startGameUI = startGameUI.GetComponent<StartGameUI>();
    }

   

    
    public void NextLevel(GameObject gameObject){
        changePlayerAngleAndDir = true;
        levelCount++;
        //cameraBehaviour.rotateCamera = true;
        gameObject.SetActive(false);
    }

    public void CollectScore(GameObject gameObject){
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
        AudioManager.Instance.PlaySound("BongeLost");
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
        SceneManager.LoadScene(sceneName);
        Debug.Log("Load " + sceneName + "!");
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

}
