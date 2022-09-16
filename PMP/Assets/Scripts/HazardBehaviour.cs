using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    public float baseMoveSpeed;
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
        hazardRigidbody.velocity = new Vector3(baseMoveSpeed, 0, 0);
        hazardXScale = transform.localScale.x / 2;
        isClose = false;
        StartCoroutine(distanceBehaviour());
    }



    IEnumerator distanceBehaviour(){

        yield return new WaitForSeconds(.5f);
        Debug.Log(transform.localScale.x);
        distanceBetween = player.position.x - transform.position.x;

        if(distanceBetween > upperDistanceBound - hazardXScale){
            hazardRigidbody.velocity = new Vector3(baseMoveSpeed * fastModeMultiplier, 0, 0);
            isClose = false;
        }else if(distanceBetween < lowerDistanceBound + hazardXScale){
            isClose = true; //Detta skickas till DangerUI som sätter igång User Interface för Danger.
        }else{
            hazardRigidbody.velocity = new Vector3(baseMoveSpeed, 0, 0);
            isClose = false;
        }

        StartCoroutine(distanceBehaviour());
    }

}
