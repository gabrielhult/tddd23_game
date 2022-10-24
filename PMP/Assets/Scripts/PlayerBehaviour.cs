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
    public Vector3 playerVelocity;
    public int climbPowerUpSpeed;
    public float swampMultiplier;
    public float arcticMultiplier;


    [SerializeField] GameObject currentClimbObject;

    private Quaternion targetRotation;
    private int currentAngle;
    private float horizontalOdd;
    private float horizontalEven;
    private Vector3 _movementForce;
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
    private float featherEffect;

    private CharacterController characterController;
    

    void Awake(){
        gameOverYPosition = 0;
        featherEffect = 1.6f;
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
        
        if(!GameManager.Instance.isGameOver){
            //Checks whether auto-run is turned on
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                autorun = !autorun;
            }
            if(GameManager.Instance.roundStarted){
                ReadInput();
            }
            
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
            if(Physics.Raycast(ray, out raycastHit, raycastDistance)){ //Make Manke lay dead in the correct pos at game over
                jumpCheckDistance = raycastHit.distance;
                gameOverYPosition = transform.position.y - raycastHit.distance;
            }
        }else {
            animator.SetBool("isDead", true);
            if(transform.position.y != gameOverYPosition){
                transform.position = new Vector3(transform.position.x, gameOverYPosition, transform.position.z);
            }
        }
    }

    private void PlayerMove(){
        if(climbing && !GameManager.Instance.cancelClimbing){
            applyMovement(_climbSpeed);
        }else{
            applyMovement(_moveSpeed);
            displayMovement("isMoving");
        }
    }

    private void applyMovement(float movementMultiplier){
        if(GameManager.Instance.isSwamp){ //Does this make climb speed weird in Swamp and Arctic
            playerVelocity = _movementForce * swampMultiplier * GameManager.Instance.gameplayScaleMultiplier;
        }else if(GameManager.Instance.isArctic){
            playerVelocity = _movementForce * arcticMultiplier * GameManager.Instance.gameplayScaleMultiplier;
        }else playerVelocity = _movementForce * movementMultiplier * GameManager.Instance.gameplayScaleMultiplier;

        if(!climbing){ 
            playerVelocity.y = ySpeed;
        }else if(GameManager.Instance.chosenPowerUp == "IncreaseClimbSpeed" && GameManager.Instance.isPowerUp && climbing){
            playerVelocity.y = climbPowerUpSpeed; 
        }else playerVelocity.y = _climbSpeed; //Makes sure swamp and arctic don't make Manke climb too fast
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    //Funkar inte riktigt, kolla här och i hur animationerna är kopplade. (gäller crawling)
    private void displayMovement(string animParameter){
        if(_movementForce != Vector3.zero){ 
            if(!crawling){
                animator.SetBool(animParameter, true);
            }
            Quaternion toRotation = Quaternion.LookRotation(_movementForce, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }else {
                if(!crawling){
                    animator.SetBool(animParameter, false);
                }else animator.SetBool("isCrawling", false);
            }
    }

    private void PlayerJump(){
        if(characterController.isGrounded){ 
            ySpeed = -0.5f;
            characterController.stepOffset = originalOffset;
            if(_jump){
                animator.SetBool("isJumping", true);
                if(GameManager.Instance.chosenPowerUp == "FeatherJump" && GameManager.Instance.isPowerUp){
                    ySpeed = _jumpSpeed * featherEffect;
                }else ySpeed = _jumpSpeed;
                
                AudioManager.Instance.PlaySound("Jump");
                _jump = false; 
            }else animator.SetBool("isJumping", false);
        }else{
                characterController.stepOffset = 0;
            }
    }

    private void PlayerClimb(){
        if(_climb && !GameManager.Instance.cancelClimbing){ //Should we climb? Dubbelchecka så detta är okej med GameManager delen
            ySpeed = -0.5f;
            if(!climbing){ //If we aren't climbing already...
                currentClimbObject = GameManager.Instance.getClimbObject();
                transform.position = new Vector3(currentClimbObject.transform.position.x, transform.position.y, currentClimbObject.transform.position.z);
                transform.LookAt(transform.position);  //Makes Manke look forward when climbinb.
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

        if(autorun){
            if(_climb){
                _movementForce = new Vector3(0f, 1f, 0f);
            }else _movementForce = new Vector3(1f, 0f, -1f * horizontal); //make character move forward automatically
        }else if(climbing){
            _movementForce = new Vector3(0f, vertical, 0f);
        }else _movementForce = new Vector3(vertical, 0f, -1f * horizontal);
        
        _movementForce.Normalize();

        if(!climbing){
            if(Input.GetButtonDown("Jump")){ //Man borde kunna hoppa medans hopp är nedtryckt, GetButton fixar detta men annat skit händer då
                _jump = true;   
            }
        }

        if(GameManager.Instance.isClimbable){
            if(vertical > 0 || (vertical >= 0 && autorun)){ //> 0 gör att man ramlar om man släpper, >= 0 gör att man står still i luften men autoclimb funkar då
                _climb = true;
            }else _climb = false;
        }else _climb = false;
    }
}
