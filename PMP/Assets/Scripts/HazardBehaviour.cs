using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
   public float moveSpeed;
   public Rigidbody rigidbody; 

    void Start(){
        rigidbody.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(moveSpeed, 0, 0);
    }

}
