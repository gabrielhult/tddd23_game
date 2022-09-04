using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 1000f;
    [SerializeField] private float _jumpMultiplier = 1000f;    

    private Rigidbody _rigidbody;
    private Vector3 _movementForce;
    private bool _jump;
    


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
        Jump();
    }

    private void ReadInput(){
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        _movementForce = new Vector3(horizontal, 0f, 0f);

        if(Input.GetButtonDown("Jump")){
            _jump = true;
        }
    }

    private void Move(){
        _rigidbody.AddForce(_movementForce * _moveSpeed);
    }

    private void Jump(){
        if(_jump){
            _rigidbody.AddForce(Vector3.up * _jumpMultiplier);
            _jump = false; //Ska göras om så dubbelhop enbart tillåts.
        }
    }
}
