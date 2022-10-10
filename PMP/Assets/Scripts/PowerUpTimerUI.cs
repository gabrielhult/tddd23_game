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
            if(!countdownStarted){
                timerUI.SetActive(true);
                timeLeft = GameManager.Instance.basePowerUpDuration * GameManager.Instance.gameplayScaleMultiplier +  GameManager.Instance.extraPowerUpDuration;
                countdownStarted = true;
            }else{ //countdown started
                timeLeft -= Time.deltaTime;
            }
            if(timeLeft < 0){
                countdownText.text = "0";
                timerUI.gameObject.SetActive(false);
                countdownStarted = false;
            }else countdownText.text = (timeLeft).ToString("0");
        }else{
            timerUI.gameObject.SetActive(false);
        }
        
    }
}
