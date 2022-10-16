using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    public float baseMoveSpeed;
    public float slowedDownMoveSpeed;
    public float swampSpeed;
    public float arcticSpeed;
    public float upperDistanceBound;
    public float lowerDistanceBound;
    public float fastModeMultiplier;


    public Rigidbody hazardRigidbody; 
    public Transform player;

    public bool isClose;

    private float distanceBetween;
    private float hazardXScale;

    void Start(){
        //Initial velocity / Base velocity
        hazardRigidbody.GetComponent<Rigidbody>();
        hazardXScale = transform.localScale.x / 2;
        isClose = false;
        StartCoroutine(distanceBehaviour());
    }



    IEnumerator distanceBehaviour(){

        yield return new WaitForSeconds(.5f);
        if(GameManager.Instance.roundStarted){
            if(GameManager.Instance.chosenPowerUp == "SlowDownHazard" && GameManager.Instance.isPowerUp){
                hazardRigidbody.velocity = new Vector3(slowedDownMoveSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
            }else if (GameManager.Instance.isSwamp){
                hazardRigidbody.velocity = new Vector3(swampSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
            }else if (GameManager.Instance.isArctic){
                hazardRigidbody.velocity = new Vector3(arcticSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
            }

            distanceBetween = player.position.x - transform.position.x;


            //TODO: Fixa så Hazard beter sig rätt, lite konstigt beteende i fart ibland
            if(distanceBetween > upperDistanceBound - hazardXScale){
                hazardRigidbody.velocity = new Vector3(baseMoveSpeed * fastModeMultiplier * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                AudioManager.Instance.StopSound("Danger");
                isClose = false;
            }else if(distanceBetween < lowerDistanceBound + hazardXScale){
                if(!GameManager.Instance.isGameOver){
                    AudioManager.Instance.PlaySound("Danger");
                }else AudioManager.Instance.StopSound("Danger");
                
                isClose = true; //Detta skickas till DangerUI som sätter igång User Interface för Danger.
            }else{
                hazardRigidbody.velocity = new Vector3(baseMoveSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                AudioManager.Instance.StopSound("Danger");
                isClose = false;
            }
        }

        StartCoroutine(distanceBehaviour());
    }

}
