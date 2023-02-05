using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject spawnBoss;

    private readonly float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;

    // medium bonus features variables
    public GameObject[] powerupPrefabs;

    private void Start()
    {

    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
        
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), transform.rotation);
        }
        if (waveNumber % 5 == 0) { Instantiate(spawnBoss, GenerateSpawnPosition(), transform.rotation); }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);
        Vector3 randomPos = new(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
