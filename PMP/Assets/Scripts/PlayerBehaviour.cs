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
    public Vector3 playerVelocity;
    private float ySpeed;
    private bool _jump;
    private bool climbing;
    private bool crawling;
    [HideInInspector]
    public bool autorun;
    private float originalOffset;
    private Animator animator;
    private float gameOverYPosition;
    private float jumpCheckDistance;
    private Ray ray;
    private RaycastHit raycastHit;

    private CharacterController characterController;
    

    void Awake(){
        gameOverYPosition = 0;
        autorun = false;
        ray.direction = Vector3.down;
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
        if(!GameManager.Instance.isGameOver && GameManager.Instance.roundStarted){
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
                jumpCheckDistance = raycastHit.distance;
                gameOverYPosition = transform.position.y - raycastHit.distance;
            }
        }else {
            animator.SetBool("isDead", true); //Kanske funkar nu?
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
        playerVelocity = _movementForce * movementMultiplier * GameManager.Instance.gameplayScaleMultiplier;
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
            //Debug.Log(jumpCheckDistance);
            if(_jump){// && jumpCheckDistance < 0.1f){
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
                transform.LookAt(transform.position);  //Makes Bonge look forward when climbinb.
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

        //Checks whether auto-run is turned on
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            autorun = !autorun;
        }

        if(climbing){
            _movementForce = new Vector3(0f, vertical, 0f);
        }else if(autorun){
            _movementForce = new Vector3(1, 0f, -1 * horizontal); //make character move forward automatically, 
        }else _movementForce = new Vector3(vertical, 0f, -1 * horizontal);
        
        _movementForce.Normalize();

        if(!climbing){
            if(Input.GetButtonDown("Jump")){ //Man borde kunna hoppa medans hopp är nedtryckt, GetButton fixar detta men annat skit händer då
                _jump = true;   
            }
        }

        if(GameManager.Instance.isClimbable){
            if(Input.GetKeyDown(KeyCode.K)){
                if(!_climb){
                    _climb = true;
                }else _climb = false;
                
            }
        }else _climb = false;

        /* if(Input.GetKeyDown(KeyCode.LeftControl)){
            if(_jump || !_climb){
                crawling = !crawling;
            }

            Debug.Log(crawling);
        } */
    }
}
