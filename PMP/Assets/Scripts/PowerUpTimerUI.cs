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
    void Update(){ //Se till så +200 inte har en counter
        if(GameManager.Instance.isPowerUp  && !GameManager.Instance.isGameOver && !GameManager.Instance.isPaused && !GameManager.Instance.increaseDistanceTimerDisabled){
            //Debug.Log(GameManager.Instance.chosenPowerUp);
            timerUI.SetActive(true);
            if(!countdownStarted){
                timeLeft = GameManager.Instance.basePowerUpDuration * GameManager.Instance.gameplayScaleMultiplier;
            }
            countdownStarted = true;
            timeLeft -= Time.deltaTime;
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
