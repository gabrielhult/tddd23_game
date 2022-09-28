using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstaclesBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject thisObstacle;
    public float upperSideWays;
    public float lowerSideways;
    public float upperUpAndDown;
    public float lowerUpAndDown;

    private bool dirRight;
    private bool dirUp;
    private bool sideways;
    private bool upanddown;


    void Awake(){
        thisObstacle = this.gameObject;
    }
    void Start(){
        if(thisObstacle.tag == "Sideways"){
            sideways = true;
            upanddown = false;

        }else if(thisObstacle.tag == "UpAndDown"){
            sideways = false;
            upanddown = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(sideways){
            if(dirRight){
                transform.Translate((Vector3.forward * speed * Time.deltaTime) / GameManager.Instance.gameplayScaleMultiplier);
            }else {
                transform.Translate((Vector3.back * speed * Time.deltaTime) / GameManager.Instance.gameplayScaleMultiplier);
            }

            if(transform.position.z >= upperSideWays){
                dirRight = false;
            }else if (transform.position.z <= lowerSideways){
                dirRight = true;
            }
        }else if(upanddown){

            if(dirUp){
                transform.Translate((Vector3.up * speed * Time.deltaTime) / GameManager.Instance.gameplayScaleMultiplier);
            }else{
                transform.Translate((Vector3.down * speed * Time.deltaTime) / GameManager.Instance.gameplayScaleMultiplier);
            }

            if(transform.position.y >= upperUpAndDown){
                Debug.Log("false");
                dirUp = false;
            }else if (transform.position.y <= lowerUpAndDown){
                dirUp = true;
            }
        }
    }
}
