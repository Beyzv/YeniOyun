using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    public GameObject branchPrefab;
    public int totalBranchCount = 15;
    public int minBranchCount = 5;
    public Vector3 minSpawnPoint;
    public Vector3 maxSpawnPoint;
    Vector3 spawnPosition;

    private void Start()
    {
        SpawnBranches();
    }

    private void SpawnBranches()
    {
        int existingBranchCount = GameObject.FindGameObjectsWithTag("branch").Length;

        if (existingBranchCount < minBranchCount)
        {
            int remainingBranchCount = totalBranchCount - existingBranchCount;

            for (int i = 0; i < remainingBranchCount; i++)
            {
                SpawnBranch();
            }
        }
    }

    private void SpawnBranch()
    {
        // Rastgele spawn konumu belirle
        spawnPosition = new Vector3(Random.Range(minSpawnPoint.x, maxSpawnPoint.x),
                                     Random.Range(minSpawnPoint.y, maxSpawnPoint.y),
                                     Random.Range(minSpawnPoint.z, maxSpawnPoint.z));

        // Yeni branch prefab'ý oluþtur
        Instantiate(branchPrefab, spawnPosition, Quaternion.identity);
    }

    // Branch yok edildiðinde yeni branch oluþturulacak fonksiyon
    public void SpawnBranchAtRandomPosition()
    {
        // Rastgele spawn konumu belirle
        spawnPosition = new Vector3(Random.Range(minSpawnPoint.x, maxSpawnPoint.x),
                                     Random.Range(minSpawnPoint.y, maxSpawnPoint.y),
                                     Random.Range(minSpawnPoint.z, maxSpawnPoint.z));

        // Yeni branch prefab'ý oluþtur
        Instantiate(branchPrefab, spawnPosition, Quaternion.identity);
    }
}
