using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float _moveSpeed;
    public float _jumpSpeed;   

    //public Rigidbody _rigidbody;

    private Quaternion targetRotation;
    public int rotationSpeed;
    private int currentAngle = 0;
    private float horizontalOdd;
    private float horizontalEven;
    private Vector3 _movementForce;
    private Vector3 playerVelocity;
    private float ySpeed;
    private bool _jump;

    private CharacterController characterController;
    

    void Awake(){
        currentAngle = -90;
        //targetRotation = Quaternion.AngleAxis(currentAngle, transform.right);
        Debug.Log("Desired angle:" + currentAngle);
    }

    // Start is called before the first frame update
    void Start() {
        //_rigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        ReadInput();
    }

    void FixedUpdate() {
        ySpeed += Physics.gravity.y * Time.deltaTime;
        PlayerMove();
        PlayerJump();
    }

    private void PlayerMove(){
        playerVelocity = _movementForce * _moveSpeed;
        playerVelocity.y = ySpeed;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void PlayerJump(){
        if(_jump){
            ySpeed = _jumpSpeed;
            //_rigidbody.AddForce(Vector3.up * _jumpMultiplier);
            _jump = false; //Ska göras om så dubbelhop enbart tillåts.
        }
    }

    private void ReadInput(){
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        
        //Debug.Log(targetRotation);

        CorrectAngleAndDirection(horizontal);

        _movementForce = new Vector3(horizontalEven, 0f, horizontalOdd); //Odd levels moves in x-direction and Even levels in z-direction.
        _movementForce.Normalize();

        if(Input.GetButtonDown("Jump")){
            _jump = true;
            Debug.Log("JUMP!");
        }
    }

    void CorrectAngleAndDirection(float horizontal){
        switch(GameManager.Instance.levelCount % 4){
            case 0:
                horizontalEven = horizontal;
                horizontalOdd = 0f;
                break;
            case 1:
                horizontalEven = 0f;
                horizontalOdd = horizontal;
                break;

            case 2:
                horizontalEven = horizontal* -1;
                horizontalOdd = 0f;
                break;

            case 3:
                horizontalEven = 0f;
                horizontalOdd = horizontal * -1;
                break;
        }
        if(GameManager.Instance.changePlayerAngleAndDir){
                //targetRotation = Quaternion.AngleAxis(currentAngle, transform.forward);
                transform.Rotate(0f, currentAngle, 0f); 
                //targetRotation = Quaternion.Euler(0f, currentAngle-90, 0f);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                //currentAngle = currentAngle - 90;
                //Debug.Log("Desired angle:" + currentAngle);
                GameManager.Instance.changePlayerAngleAndDir = false;
            }    
    }
    
}
