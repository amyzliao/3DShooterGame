using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject StationaryTreePrefab;
    public GameObject EnemyTreePrefab;
    public int StationaryTreeCount = 5;
    public int EnemyTreeCount = 3;
    public Vector3 SpawnAreaSize = new Vector3(20, 0, 20);
    public float MinSpawnDistanceFromPlayer = 5f;

    private List<TreeBase> _trees = new();
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player")?.transform;

        // spawn stationary trees
        for (int i = 0; i < StationaryTreeCount; i++)
        {
            SpawnTree(StationaryTreePrefab);
        }

        // spawn enemy trees
        for (int i = 0; i < EnemyTreeCount; i++)
        {
            SpawnTree(EnemyTreePrefab);
        }
    }

    private void Update()
    {
        foreach (var tree in _trees)
        {
            if (tree)
            {
                tree.UpdateBehavior();
            }
        }
    }

    private void SpawnTree(GameObject treePrefab)
    {
        Vector3 spawnPosition;

        // ensure the tree spawns a certain distance away from the player
        int maxAttempts = 100;
        int attempts = 0;
        do
        {
            spawnPosition = new Vector3(
                Random.Range(-SpawnAreaSize.x, SpawnAreaSize.x),
                0,
                Random.Range(-SpawnAreaSize.z, SpawnAreaSize.z)
            );
            attempts++;
        } while (Vector3.Distance(spawnPosition, _player.position) < MinSpawnDistanceFromPlayer && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            return;
        }

        GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
        if (tree.TryGetComponent<TreeBase>(out var treeBehavior))
        {
            _trees.Add(treeBehavior);
        }
    }
}
