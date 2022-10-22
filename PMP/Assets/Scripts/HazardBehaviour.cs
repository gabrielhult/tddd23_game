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
    private bool speedNerfed;

    void Start(){
        //Initial velocity / Base velocity
        hazardRigidbody.GetComponent<Rigidbody>();
        hazardXScale = transform.localScale.x / 2;
        isClose = false;
        speedNerfed = false;
        StartCoroutine(distanceBehaviour());
    }



    IEnumerator distanceBehaviour(){

        //TODO: Work on this / balance

        yield return new WaitForSeconds(.5f);

        if(GameManager.Instance.isGameOver){
            hazardRigidbody.velocity = new Vector3(5f * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
        }else if(GameManager.Instance.roundStarted){
            /* if(GameManager.Instance.chosenPowerUp != "SlowDownHazard"){
                hazardRigidbody.velocity = new Vector3(baseMoveSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);   
            } */

            if(GameManager.Instance.chosenPowerUp == "SlowDownHazard" && GameManager.Instance.isPowerUp){
                hazardRigidbody.velocity = new Vector3(slowedDownMoveSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                speedNerfed = true;
            }else if (GameManager.Instance.isSwamp){
                hazardRigidbody.velocity = new Vector3(swampSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                speedNerfed = false;
            }else if (GameManager.Instance.isArctic){
                hazardRigidbody.velocity = new Vector3(arcticSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                speedNerfed = false;
            }else{
                hazardRigidbody.velocity = new Vector3(baseMoveSpeed * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                speedNerfed = false;
            }

            distanceBetween = player.position.x - transform.position.x;

            if(distanceBetween > upperDistanceBound - hazardXScale && !speedNerfed){
                hazardRigidbody.velocity = new Vector3(baseMoveSpeed * fastModeMultiplier * GameManager.Instance.gameplayScaleMultiplier, 0, 0);
                AudioManager.Instance.StopSound("Danger");
                isClose = false;
            }else if(distanceBetween < lowerDistanceBound + hazardXScale){
                if(!GameManager.Instance.isGameOver){
                    AudioManager.Instance.PlaySound("Danger");
                }else AudioManager.Instance.StopSound("Danger");
                
                isClose = true; //Detta skickas till DangerUI som sätter igång User Interface för Danger.
            }else{
                AudioManager.Instance.StopSound("Danger");
                isClose = false;
            }
        }
        //Debug.Log("Velocity: " + hazardRigidbody.velocity);
        StartCoroutine(distanceBehaviour());
    }

}
