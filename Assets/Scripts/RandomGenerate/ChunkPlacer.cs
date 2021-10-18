using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkPlacer : MonoBehaviour
{
    public float offsetForSpawn = 90;
    public Transform player;
    public Chunk[] ChunkPrefabs;
    public Chunk firstChunk;
    public List<Chunk> spawnedChunks = new List<Chunk>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawnedChunks.Add(firstChunk);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z > spawnedChunks[spawnedChunks.Count - 1].End.position.z-95)
        {
            SpawnChunks();
        }
    }

    private void SpawnChunks()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        //Add to List
        spawnedChunks.Add(newChunk);
        //delete
        if(spawnedChunks.Count >= 4)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }

    }
}
