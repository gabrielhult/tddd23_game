using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    PlayerCollision playerCollision;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerCollision = player.GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCollision.collidedObjectTag == "Goal"){
            Debug.Log("Next Level!");
        }
    }
}
