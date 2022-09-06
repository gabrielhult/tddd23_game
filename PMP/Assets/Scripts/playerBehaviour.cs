using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float _moveSpeed;
    public float _jumpSpeed;   
    public int rotationSpeed;
    public float gravityMagnitude;

    //public Rigidbody _rigidbody;

    private Quaternion targetRotation;
    private int currentAngle;
    private float horizontalOdd;
    private float horizontalEven;
    private Vector3 _movementForce;
    private Vector3 playerVelocity;
    private float ySpeed;
    private bool _jump;
    private float originalOffset;

    //private int jumpCounter;
    //public int maxJumpAllowed;

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
        originalOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update() {
        ReadInput();
    }

    void FixedUpdate() {
        ySpeed += Physics.gravity.y * gravityMagnitude * Time.deltaTime; //Fixa bugg att den adderar när man står på Obstacle, se bra video
        PlayerMove();
        PlayerJump();
    }

    private void PlayerMove(){
        playerVelocity = _movementForce * _moveSpeed;
        playerVelocity.y = ySpeed;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void PlayerJump(){
        //Lägg till dubbelhopp och isGrounded
        if(_jump){
            ySpeed = _jumpSpeed;
            _jump = false; 
        }
    }

    private void ReadInput(){
        float horizontal = Input.GetAxisRaw("Horizontal");

        CorrectAngleAndDirection(horizontal);

        _movementForce = new Vector3(horizontalEven, 0f, horizontalOdd); //Odd levels moves in x-direction and Even levels in z-direction.
        _movementForce.Normalize();

        if(Input.GetButtonDown("Jump")){ //Ser till så vi bara kan hoppa när vi är på marken
            if(characterController.isGrounded){
                ySpeed = -0.5f;
                characterController.stepOffset = originalOffset;
                _jump = true;
            }else{
                characterController.stepOffset = 0;
            }
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
