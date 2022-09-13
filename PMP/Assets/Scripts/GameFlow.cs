using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public Transform firstMainTile;
    public Transform obstacleObject;
    public PlayerBehaviour playerBehaviour;
    private Vector3 nextMainTileSpawn;
    private Vector3 nextObstacleSpawn;
    private float currentVelocityX;
    private int randZObstacleIndex;



    // Start is called before the first frame update
    void Start()
    {
        nextMainTileSpawn.x = 44;
        StartCoroutine(spawnMainTile());
        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnMainTile(){
        if(playerBehaviour.playerVelocity.x < 0.5){
            currentVelocityX = 1;
        }else currentVelocityX = playerBehaviour.playerVelocity.x;
        yield return new WaitForSeconds(1 / (currentVelocityX / 1.4f)); //Optimera detta
        randZObstacleIndex = Random.Range(-6, 7);
        nextObstacleSpawn = nextMainTileSpawn;
        nextObstacleSpawn.z = randZObstacleIndex;
        Instantiate(obstacleObject, nextObstacleSpawn, obstacleObject.rotation);
        Instantiate(firstMainTile, nextMainTileSpawn, firstMainTile.rotation);
        nextMainTileSpawn.x += 4;
        StartCoroutine(spawnMainTile());
    }
}
