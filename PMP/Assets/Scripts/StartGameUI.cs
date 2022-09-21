using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGameUI : MonoBehaviour
{
    public float timeLeft;
    private float timeLeftStartValue;
    public TextMeshProUGUI countdownText;
    public bool isCountdown;

    void Awake(){
        timeLeftStartValue = timeLeft;
        isCountdown = true;
    }

    // Update is called once per frame
    void Update(){
        if(isCountdown){
            timeLeft -= Time.deltaTime;
            countdownText.text = "Get ready to run!\n" + (timeLeft).ToString("0"); 
            if(timeLeft < 0){
                isCountdown = false;
                countdownText.gameObject.SetActive(false);
                GameManager.Instance.roundStarted = true;
            }
        }
        
    }
}
