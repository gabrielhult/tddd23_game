using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{

    public GameObject gameplayMenu;
    public GameObject powerUpMenu;
    public GameObject doubleBananaUI;
    public GameObject increaseDistanceUI;
    public GameObject noObstaclesUI;
    public GameObject slowHazardUI;
    public GameObject featherJumpUI;

    public GameObject climbSpeedUI;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver){
            powerUpMenu.SetActive(false);
            gameplayMenu.SetActive(false);
        }

        if(GameManager.Instance.isPowerUp){
            if(GameManager.Instance.chosenPowerUp == "IncreaseDistanceAward"){ //IEnumerator behövs för att inte logik och UI krockar
                increaseDistanceUI.SetActive(true);     
                StartCoroutine(showDistanceAward());
            }else increaseDistanceUI.SetActive(false);

            if(GameManager.Instance.chosenPowerUp == "DoubleBananaScoreCollect"){
                doubleBananaUI.SetActive(true);
            }else doubleBananaUI.SetActive(false);

            if(GameManager.Instance.chosenPowerUp == "NoObstacles"){
                noObstaclesUI.SetActive(true);
            }else noObstaclesUI.SetActive(false);

            if(GameManager.Instance.chosenPowerUp == "SlowDownHazard"){
                slowHazardUI.SetActive(true);
            }else slowHazardUI.SetActive(false);

            if(GameManager.Instance.chosenPowerUp == "FeatherJump"){
                featherJumpUI.SetActive(true);
            }else featherJumpUI.SetActive(false);

            if(GameManager.Instance.chosenPowerUp == "IncreaseClimbSpeed"){
                climbSpeedUI.SetActive(true);
            }else climbSpeedUI.SetActive(false);
        }else{
            increaseDistanceUI.SetActive(false);
            doubleBananaUI.SetActive(false);
            noObstaclesUI.SetActive(false);
            slowHazardUI.SetActive(false);
            featherJumpUI.SetActive(false);
            climbSpeedUI.SetActive(false);
        }
        
    }

    IEnumerator showDistanceAward(){

        yield return new WaitForSeconds(1);
        GameManager.Instance.chosenPowerUp = "";
        GameManager.Instance.distanceBonusLimiter = true;
        

    }
}
