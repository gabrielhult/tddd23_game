using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpTimerUI : MonoBehaviour
{
    private float timeLeft;
    private float timeLeftStartValue;
    public GameObject timerUI;
    public TextMeshProUGUI countdownText;
    private bool countdownStarted;

    void Awake(){
        countdownStarted = false;
    }

    // Update is called once per frame
    void Update(){ 
        if(GameManager.Instance.isPowerUp  && !GameManager.Instance.isGameOver && !GameManager.Instance.isPaused && !GameManager.Instance.increaseDistanceTimerDisabled){
            timerUI.SetActive(true);
            if(!countdownStarted){
                timeLeft = GameManager.Instance.basePowerUpDuration * GameManager.Instance.gameplayScaleMultiplier +  GameManager.Instance.extraPowerUpDuration;
                Debug.Log("Time left: " + timeLeft);
                countdownStarted = true;
            }
            if(countdownStarted){
                timeLeft -= Time.deltaTime;
            }
            if(timeLeft < 0){
                countdownText.text = "0";
                timerUI.gameObject.SetActive(false);
                if(GameManager.Instance.chosenPowerUp == ""){ //Check if this logic is working better with the timer than before
                    countdownStarted = false;
                }
            }else countdownText.text = (timeLeft).ToString("0");
        }else{
            timerUI.gameObject.SetActive(false);
        }
        
    }
}
