using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabArray;
    private float spawnRangeX = 10;
    private float spawnPositionZ = 30;
    private float aggressiveSpawnBottomZ = 0;
    private float aggressiveSpawnTopZ = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        //Spawn an animal in a random location starting at 2 seconds, every 1.5 seconds.
        InvokeRepeating("SpawnRandomAnimal", 2, 1.5f);
        InvokeRepeating("SpawnAggressiveAnimalRight", 2.5f, 3.2f);
        InvokeRepeating("SpawnAggressiveAnimalLeft", 2.7f, 3.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        //Animal should spawn in range that player can reach.
        int animalIndex = Random.Range(0, prefabArray.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPositionZ);
        Instantiate(prefabArray[animalIndex], spawnPosition, prefabArray[animalIndex].transform.rotation);
    }

    void SpawnAggressiveAnimalRight()
    {
        //Animal Spawns from right side and can hit player
        int animalIndex = Random.Range(0, prefabArray.Length);
        Vector3 spawnPosition = new Vector3(-20, 0, Random.Range(aggressiveSpawnBottomZ, aggressiveSpawnTopZ));
        GameObject gameObj = (GameObject) Instantiate(prefabArray[animalIndex], spawnPosition, prefabArray[animalIndex].transform.rotation);
        gameObj.transform.Rotate(new Vector3(0, 270, 0));
    }

    void SpawnAggressiveAnimalLeft()
    {
        //Animal Spawns from left side and can hit player
        int animalIndex = Random.Range(0, prefabArray.Length);
        Vector3 spawnPosition = new Vector3(20, 0, Random.Range(aggressiveSpawnBottomZ, aggressiveSpawnTopZ));
        GameObject gameObj = (GameObject) Instantiate(prefabArray[animalIndex], spawnPosition, prefabArray[animalIndex].transform.rotation);
        gameObj.transform.Rotate(new Vector3(0, 90, 0));
    }
}
