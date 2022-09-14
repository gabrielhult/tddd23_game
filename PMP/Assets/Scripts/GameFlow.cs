using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public Transform[] nextTile;
    public Transform[] smallObjects;
    public Transform[] largeObjects;
    public PlayerBehaviour playerBehaviour;
    public int obstacleSpawnRateIndex;
    public int maxLoadedChunks;
    private Queue<Transform> chunksQueue;
    private Transform newChunk;
    private Transform tempChunk; //Chunk to be removed from the game
    private int randObstacleIndex;
    private Vector3 nextMainTileSpawn;
    private Vector3 nextObstacleSpawn;
    private float currentVelocityX;
    private int randZPos;
    private int randLargeObjSpawn;
    private int obstacleSpawnCounter;
    private bool isLargeObjectSpawned;


    private int tileRand;
    private int holeSpawnCounter;
    public int holeSpawnRateIndex;
  



    // Start is called before the first frame update
    void Start()
    {
        nextMainTileSpawn.x = 44;
        StartCoroutine(spawnTile());
        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
        chunksQueue = new Queue<Transform>();
    }

    void tileSpawn(){
        holeSpawnCounter++;
        if(holeSpawnCounter % holeSpawnRateIndex == 0){
            tileRand = Random.Range(0, nextTile.Length);
        }else tileRand = 0;

        newChunk = Instantiate(nextTile[tileRand], nextMainTileSpawn, nextTile[tileRand].rotation);

        //Not needed but stays here for one push so I can see it.
        /* if(chunksQueue.Count > maxLoadedChunks){ //If the queue has reached the amount we accept.
            tempChunk = chunksQueue.Dequeue();
            tempChunk.gameObject.SetActive(false);
        }
        chunksQueue.Enqueue(newChunk); //Enter the new tile into our queue. */
        
        nextObstacleSpawn = nextMainTileSpawn;
    }

    void obstacleSpawn(){
        //Spawns an obstacle every "obstacleSpawnRateIndex" tile
        obstacleSpawnCounter++;
        if(obstacleSpawnCounter % obstacleSpawnRateIndex == 0){
            randLargeObjSpawn = Random.Range(0, 5);
            if(randLargeObjSpawn == 0){
                largeObjectSpawn();
                isLargeObjectSpawned = true;
            }else isLargeObjectSpawned = false;
            
            if(!isLargeObjectSpawned){
                locationObstacle();
                smallObjectSpawn();
            }
            obstacleSpawnCounter = 0;
        }
    }

    void locationObstacle(){
        //Randomly chooses where to place the next obstacle; left, center or right. -6 = left, 0 = center, 6 = right
        randZPos = Random.Range(-1, 2) * 6;
        nextObstacleSpawn.z = randZPos;
    }

    void smallObjectSpawn(){
        //Randomly chooses an obstacle type from GameManager array
        randObstacleIndex = Random.Range(0, smallObjects.Length);
        //Spawns an object from the "Small Object"-array.
        Instantiate(smallObjects[randObstacleIndex], nextObstacleSpawn, smallObjects[randObstacleIndex].rotation);
    }

    /* Large Objects should ALWAYS spawned in the middle */
    void largeObjectSpawn(){
        nextObstacleSpawn.z = 0;
        //Randomly chooses an obstacle type from GameManager array
        randObstacleIndex = Random.Range(0, largeObjects.Length);
        //Spawns an object from the "Small Object"-array.
        Instantiate(largeObjects[randObstacleIndex], nextObstacleSpawn, largeObjects[randObstacleIndex].rotation);
    }

    IEnumerator spawnTile(){
        //Logic for how often new tiles spawn.
        /* if(playerBehaviour.playerVelocity.x < 0.5){
            currentVelocityX = 1;
        }else currentVelocityX = playerBehaviour.playerVelocity.x;
        yield return new WaitForSeconds(1 / (currentVelocityX / 1.4f)); //Optimera detta */
        yield return new WaitForSeconds(.3f); //Optimera detta
        if(!GameManager.Instance.isGameOver){
            tileSpawn();

            obstacleSpawn();

            nextMainTileSpawn.x += 4;
            StartCoroutine(spawnTile()); //Makes this IEnumerator loop.
        }
    }
}
