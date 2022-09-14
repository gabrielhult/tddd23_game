using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
   public float moveSpeed;
   public Rigidbody hazardRigidbody; 

    void Start(){
        hazardRigidbody.GetComponent<Rigidbody>();
        hazardRigidbody.velocity = new Vector3(moveSpeed, 0, 0);
    }

}
