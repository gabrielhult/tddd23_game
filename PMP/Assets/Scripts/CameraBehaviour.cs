using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;


    void Start(){
        transform.position = target.position + offset;
        AudioManager.Instance.PlaySound("GameMusic");
        //GetComponent<Rigidbody>().velocity = new Vector3(3,0,0); //Kanske kör på detta istället?
    }

    void Update(){
        if(!GameManager.Instance.isGameOver){
            ReadCameraInput();
        }else{
            AudioManager.Instance.StopSound("GameMusic");
            //AudioManager.Instance.PlaySound("GameOver");
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

    void ReadCameraInput(){
        if(Input.GetKeyDown(KeyCode.L)){
            offset.x = -offset.x;
            transform.Rotate(80, 180, 0);
        }else if(Input.GetKeyUp(KeyCode.L)){
            offset.x = -offset.x;
            transform.Rotate(80, 180, 0);
        }
    }
}
