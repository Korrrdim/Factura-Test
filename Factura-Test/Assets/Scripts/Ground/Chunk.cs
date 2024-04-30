using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private List<Vector3> _points = new List<Vector3>();

    [SerializeField] private int _pointCount;
    [SerializeField] private Vector2 _offsetZ;
    [SerializeField] private Vector2 _offsetX;
    [Space]
    [SerializeField] private int _enemyCount;
    [SerializeField] private Vector2 _enemyOffsetX;
    [SerializeField] private Enemy _enemy;


    public List<Vector3> GetPoints => _points;

    private void Awake()
    {
        GeneratePath();
        GenerateEnemy();
    }

    private void GeneratePath()
    {
        Vector3 point = Vector3.zero;

        float step = _offsetZ.y * 2 / _pointCount;

        for (var i = transform.position.z + _offsetZ.x; i < transform.position.z + _offsetZ.y; i += step)
        {
            point.x = Random.Range(_offsetX.x, _offsetX.y);
            point.z = i;
            _points.Add(point);
        }
    }

    private void GenerateEnemy()
    {
        if (_enemyCount == 0) return;

        Vector3 position = Vector3.zero;
        float step = _offsetZ.y * 2 / _enemyCount;

        for (var i = transform.position.z + _offsetZ.x; i < transform.position.z + _offsetZ.y; i += step)
        {
            position.x = Random.Range(_enemyOffsetX.x, _enemyOffsetX.y);
            position.z = i;
            Instantiate(_enemy, position, Quaternion.identity, transform);
        }
    }
}
