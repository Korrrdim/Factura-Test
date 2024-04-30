using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathGenerator))]
public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private Chunk _baseChunk;
    [SerializeField] private Chunk _finishChunk;
    [Space]
    [SerializeField, Range(0, 25)] private int _chunksCount;
    private float _offsetZ = 36.75f;
    private PathGenerator _pathGenerator;
    private List<Vector3> _allPoints = new List<Vector3>();
    private List<Chunk> _chunks = new List<Chunk>();

    private void Awake()
    {
        _pathGenerator = GetComponent<PathGenerator>();

        GenerateChunk(_startChunk);
        for (int i = 1; i < _chunksCount - 1; i++)
        {
            GenerateChunk(_baseChunk);
        }
        GenerateChunk(_finishChunk);

        _pathGenerator.GeneratePath(_allPoints);
    }

    private void GenerateChunk(Chunk spawnedChunk)
    {
        Vector3 position = _chunks.Count >= 1 ? _chunks[_chunks.Count - 1].transform.position + _offsetZ * Vector3.forward : Vector3.zero;
        var chunk = Instantiate(spawnedChunk, position, Quaternion.identity, transform);

        _chunks.Add(chunk);
        _allPoints.AddRange(chunk.GetPoints);
    }
}
