using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float _moveSpeed;
    public float _climbSpeed;
    public float _jumpSpeed;  
    public float _crawlSpeed; 
    public int rotationSpeed;
    public float gravityMagnitude;
    public bool _climb;
    public float raycastDistance;

    [SerializeField] GameObject currentClimbObject;

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
    private bool crawling;
    private float originalOffset;
    private Animator animator;
    private float gameOverYPosition;
    private Ray ray;
    private RaycastHit raycastHit;

    //private int jumpCounter;
    //public int maxJumpAllowed;

    private CharacterController characterController;
    

    void Awake(){
        //currentAngle = -90;
        //targetRotation = Quaternion.AngleAxis(currentAngle, transform.right);
        //Debug.Log("Desired angle:" + currentAngle);
        gameOverYPosition = 0;
    }

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        originalOffset = characterController.stepOffset;
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
        ray.direction = Vector3.down;
    }

    // Update is called once per frame
    void Update() {
        if(!GameManager.Instance.isGameOver){
            ReadInput();
        }
    }

    void FixedUpdate() {
        ySpeed += Physics.gravity.y * gravityMagnitude * Time.deltaTime;
        ray.origin = transform.position;
        if(!GameManager.Instance.isGameOver){
            animator.SetBool("isDead", false);
            PlayerMove();
            PlayerJump();
            PlayerClimb();
            if(Physics.Raycast(ray, out raycastHit, raycastDistance)){
                gameOverYPosition = transform.position.y - raycastHit.distance; //Funkar fortfarande inte riktigt...
                /* Debug.Log("Transform pos: " + transform.position.y);
                Debug.Log("Distance: " + raycastHit.distance);
                Debug.Log("GO Pos: " +  gameOverYPosition); */
            }
        }else {
            animator.SetBool("isDead", true); //Fixa så kropp alltid ligger ner på marken vid GameOver
            if(transform.position.y != gameOverYPosition){
                transform.position = new Vector3(transform.position.x, gameOverYPosition, transform.position.z);
            }
        }
    }

    private void PlayerMove(){
        if(climbing){
            applyMovement(_climbSpeed);
        }else if(crawling){
            applyMovement(_crawlSpeed);
            displayMovement("isCrawling");
        }else{
            applyMovement(_moveSpeed);
            displayMovement("isMoving");
        }
    }

    private void applyMovement(float movementMultiplier){
        playerVelocity = _movementForce * movementMultiplier;
        if(!climbing){ //&&!crawling?
            playerVelocity.y = ySpeed;
        } 
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    //Funkar inte riktigt, kolla här och i hur animationerna är kopplade.
    private void displayMovement(string animParameter){
        if(_movementForce != Vector3.zero){ 
            if(!crawling){
                animator.SetBool(animParameter, true);
            }else animator.SetBool("isCrawling", true);
                
                Quaternion toRotation = Quaternion.LookRotation(_movementForce, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }else {
                if(!crawling){
                    animator.SetBool(animParameter, false);
                }else animator.SetBool("isCrawling", false);
            }
    }

    private void PlayerJump(){
        //Lägg KANSKE till dubbelhopp
        if(characterController.isGrounded){ //&& !crawling?
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
            ySpeed = -0.5f;
            if(!climbing){ //If we aren't climbing already...
                currentClimbObject = GameManager.Instance.getClimbObject();
                transform.position = new Vector3(currentClimbObject.transform.position.x, transform.position.y, currentClimbObject.transform.position.z);
                transform.LookAt(transform.forward);  //Makes Bonge look forward when climbinb.
                climbing = true;
            }if(_movementForce != Vector3.zero){
                animator.SetBool("isClimbing", true);
                animator.speed = 1;
            }else animator.speed = 0;
            
        }else {
            animator.SetBool("isClimbing", false);
            climbing = false;
            animator.speed = 1;
        }
    }

    private void ReadInput(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //CorrectDirection(horizontal, vertical); 

        if(climbing){
            _movementForce = new Vector3(0f, vertical, 0f);
        }else _movementForce = new Vector3(vertical, 0f, -1 * horizontal); //Odd levels moves in x-direction and Even levels in z-direction.
        
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
        }else _climb = false;

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            if(_jump || !_climb){
                crawling = !crawling;
            }

            Debug.Log(crawling);
        }
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
