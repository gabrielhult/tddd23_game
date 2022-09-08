using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float _moveSpeed;
    public float _jumpSpeed;   
    public int rotationSpeed;
    public float gravityMagnitude;
    public bool _climb;

    [SerializeField] GameObject tempClimbObject;

    //public Rigidbody _rigidbody;

    private Quaternion targetRotation;
    private int currentAngle;
    private float horizontalOdd;
    private float horizontalEven;
    private Vector3 _movementForce;
    private Vector3 playerVelocity;
    private float ySpeed;
    private bool _jump;
    private bool climbing;
    private float originalOffset;
    private Animator animator;

    //private int jumpCounter;
    //public int maxJumpAllowed;

    private CharacterController characterController;
    

    void Awake(){
        //currentAngle = -90;
        //targetRotation = Quaternion.AngleAxis(currentAngle, transform.right);
        //Debug.Log("Desired angle:" + currentAngle);
    }

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        originalOffset = characterController.stepOffset;
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
    }

    // Update is called once per frame
    void Update() {
        if(!GameManager.Instance.isGameOver){
            ReadInput();
        }
    }

    void FixedUpdate() {
        ySpeed += Physics.gravity.y * gravityMagnitude * Time.deltaTime; //Fixa bugg att den adderar när man står på Obstacle, se bra video
        if(!GameManager.Instance.isGameOver){
            animator.SetBool("isDead", false);
            PlayerMove();
            PlayerJump();
            PlayerClimb();
        }else animator.SetBool("isDead", true);
    }

    private void PlayerMove(){
        playerVelocity = _movementForce * _moveSpeed;
        
        if(climbing){
            characterController.Move(playerVelocity * Time.deltaTime);
        }else{
            playerVelocity.y = ySpeed;
            characterController.Move(playerVelocity * Time.deltaTime);
            if(_movementForce != Vector3.zero){ 
                animator.SetBool("isMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(_movementForce, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }else animator.SetBool("isMoving", false);
        }
    }

    private void PlayerJump(){
        //Lägg till dubbelhopp
        if(characterController.isGrounded){

            ySpeed = -0.5f;
            characterController.stepOffset = originalOffset;
            
            if(_jump){
                animator.SetBool("isJumping", true);
                ySpeed = _jumpSpeed;
                _jump = false; 
            }else animator.SetBool("isJumping", false);
        }else{
                characterController.stepOffset = 0;
            }
    }

    private void PlayerClimb(){
        if(_climb){ //Should we climb?
            if(!climbing){ //If we aren't climbing already...
                tempClimbObject = GameManager.Instance.getClimbObject();
                transform.position = new Vector3(tempClimbObject.transform.position.x, transform.position.y, tempClimbObject.transform.position.z);
                //TODO: Rotate always towards end of level direction.
                //transform.rotation = new Quaternion(tempClimbObject.transform.rotation.x, 90 , tempClimbObject.transform.rotation.z)
                climbing = true;
            }
            animator.SetBool("isClimbing", true);
        }else {
            animator.SetBool("isClimbing", false);
            climbing = false;
        }
    }

    private void ReadInput(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        CorrectDirection(horizontal, vertical); 
        if(climbing){
            _movementForce = new Vector3(0f, horizontal, 0f);
        }else _movementForce = new Vector3(horizontalEven, 0f, horizontalOdd); //Odd levels moves in x-direction and Even levels in z-direction.
        
        _movementForce.Normalize();

        if(Input.GetButtonDown("Jump")){
                _jump = true;
        }
        if(GameManager.Instance.isClimbable){
            if(Input.GetKeyDown(KeyCode.K)){
                if(!_climb){
                    _climb = true;
                }else _climb = false;
                
            }
            //Debug.Log(_climb); //TODO: Ska bli false om man lämnar collisionområdet, climb bool logik konstig atm. 
        }else _climb = false;
    }
        

    void CorrectDirection(float horizontal, float vertical){
        switch(GameManager.Instance.levelCount % 4){
            case 0:
                horizontalEven = horizontal;
                horizontalOdd = vertical;
                break;
            case 1:
                horizontalEven = vertical * -1;
                horizontalOdd = horizontal;
                break;

            case 2:
                horizontalEven = horizontal* -1;
                horizontalOdd = vertical * -1;
                break;

            case 3:
                horizontalEven = vertical;
                horizontalOdd = horizontal * -1;
                break;
        } 
    }
    
}
