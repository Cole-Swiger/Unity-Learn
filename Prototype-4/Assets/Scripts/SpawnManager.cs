using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject projectile;
    private float spawnRangeX = 9;
    private float spawnRangeZ = 9;
    public float enemyCount;
    public int waveNumber = 1;
    private bool isBossAlive = false;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefabs[0], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private void SpawnEnemyWave(int enemies)
    {
        if (enemies % 5 == 0)
        {
            Instantiate(enemyPrefabs[2], GenerateSpawnPosition(), enemyPrefabs[2].transform.rotation);
            isBossAlive = true;
            StartCoroutine(SpawnMinions());
        }
        else
        {
            if (waveNumber % 5 != 0) isBossAlive = false;
            for (int i = 0; i < enemies; i++)
            {
                float spawnProbability = Random.Range(0, 100);
                int prefabIndex;
                prefabIndex = spawnProbability < 75 ? 0 : 1;

                Instantiate(enemyPrefabs[prefabIndex], GenerateSpawnPosition(), enemyPrefabs[prefabIndex].transform.rotation);
            }
        } 
    }

    private void SpawnPowerup()
    {
        int spawnIndex = Random.Range(0, 3);
        Debug.Log("Spawn index: " + spawnIndex);
        Instantiate(powerupPrefabs[spawnIndex], GenerateSpawnPosition(), powerupPrefabs[spawnIndex].transform.rotation);
    }

    public void SpawnProjectiles()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Vector3 enemyPos = transform.position;
            Vector3 spawnPosition = playerPos;// (playerPos - enemyPos).normalized * 1.5f;
            Vector3 spawnDirection = (enemyPos - playerPos).normalized * 1.5f;
            GameObject proj = Instantiate(projectile, spawnPosition + spawnDirection, projectile.transform.rotation);
            proj.GetComponent<ProjectileController>().enemyToFollow = enemy;
        }
    }

    private IEnumerator SpawnMinions()
    {
        while (isBossAlive)
        {
            SpawnEnemyWave(2);
            yield return new WaitForSeconds(8);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
