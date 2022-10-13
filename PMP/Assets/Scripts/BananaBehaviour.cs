using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBehaviour : MonoBehaviour
{
    public GameObject thisBanana;

    public GameObject powerUpObject;
    public GameObject doubleBananaUI;
    //public GameObject secondBanana;
    public GameObject increaseDistanceUI;
    public GameObject noObstaclesUI;
    public GameObject slowHazardUI;
    public GameObject featherJumpUI;

    public GameObject climbSpeedUI;
    // Start is called before the first frame update
    void Start()
    {
        thisBanana = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.closePowerUp){
            powerUpObject.SetActive(true);
        }else powerUpObject.SetActive(false);

        if(GameManager.Instance.chosenPowerUp == "IncreaseDistanceAward"){
            increaseDistanceUI.SetActive(true);     
        }else increaseDistanceUI.SetActive(false);

        if(GameManager.Instance.chosenPowerUp == "DoubleBananaScoreCollect"){
            doubleBananaUI.SetActive(true); //TODO: Add effect to banana when double score is active!
            //secondBanana.SetActive(true);
        }else {
            doubleBananaUI.SetActive(false);
            //secondBanana.SetActive(false);
        }

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
    }

    private void OnTriggerEnter(Collider other){ //TODO: Fixa detta, funkar ej ännu, vet ej varför dock
        if(other.tag == "Obstacle"){
            Debug.Log("banana collide with obstacle");
            thisBanana.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Hazard"){
            if(!GameManager.Instance.isGameOver){
                Destroy(thisBanana);
            }
        }
    }
}
