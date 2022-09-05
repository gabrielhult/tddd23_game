using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 1000f;
    [SerializeField] private float _jumpMultiplier = 1000f;    

    public Rigidbody _rigidbody;

    private Quaternion targetRotation;
    public int rotationSpeed;
    private int currentAngle = 0;
    private float horizontalOdd;
    private float horizontalEven;
    private Vector3 _movementForce;
    private bool _jump;
    

    void Awake(){
        currentAngle = -90;
        //targetRotation = Quaternion.AngleAxis(currentAngle, transform.right);
        Debug.Log("Desired angle:" + currentAngle);
    }

    // Start is called before the first frame update
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        ReadInput();
    }

    void FixedUpdate() {
        Move();
        //Rotate();
        Jump();
    }

    private void Move(){
        _rigidbody.velocity = _movementForce * _moveSpeed;
    }

    private void Jump(){
        if(_jump){
            _rigidbody.AddForce(Vector3.up * _jumpMultiplier);
            _jump = false; //Ska göras om så dubbelhop enbart tillåts.
        }
    }

    private void ReadInput(){
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        
        //Debug.Log(targetRotation);

        CorrectAngleAndDirection(horizontal);

        _movementForce = new Vector3(horizontalEven, 0f, horizontalOdd); //Odd levels moves in x-direction and Even levels in z-direction.

        if(Input.GetButtonDown("Jump")){
            _jump = true;
        }
    }

    void CorrectAngleAndDirection(float horizontal){
        //Byt ut dessa cases mot en switch-statement och refactor till annan funktion, rotation funkar inte helt rätt atm
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

                //Debug.Log("Desired angle:" + currentAngle);
                GameManager.Instance.changePlayerAngleAndDir = false;
            }    
    }
    
}
