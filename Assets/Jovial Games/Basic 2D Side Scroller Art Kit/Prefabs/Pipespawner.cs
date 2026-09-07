using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform pipePairPrefab; 
    [SerializeField] private Transform bird;

    [Header("Spawn settings")]
    [SerializeField] private float spawnDistanceAhead = 15f; 
    [SerializeField] private float pipeSpacing = 4f;        
    [SerializeField] private float minHeight = -2f;          
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float despawnDistanceBehind = 15f; 

    private float nextSpawnX;
    private readonly List<Transform> activePipes = new List<Transform>();

    private void Start()
    {
        nextSpawnX = bird.position.x + spawnDistanceAhead;

 
        while (nextSpawnX < bird.position.x + spawnDistanceAhead + pipeSpacing * 2f)
        {
            SpawnPipe();
        }
    }

    private void Update()
    {
        while (bird.position.x + spawnDistanceAhead > nextSpawnX)
        {
            SpawnPipe();
        }

        for (int i = activePipes.Count - 1; i >= 0; i--)
        {
            if (activePipes[i] == null)
            {
                activePipes.RemoveAt(i);
                continue;
            }

            if (activePipes[i].position.x < bird.position.x - despawnDistanceBehind)
            {
                Destroy(activePipes[i].gameObject);
                activePipes.RemoveAt(i);
            }
        }
    }

    private void SpawnPipe()
    {
        float y = Random.Range(minHeight, maxHeight);
        Transform pipe = Instantiate(pipePairPrefab, new Vector3(nextSpawnX, y, 0f), Quaternion.identity);
        activePipes.Add(pipe);
        nextSpawnX += pipeSpacing;
    }
}