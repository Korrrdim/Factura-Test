using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class PathGenerator : MonoBehaviour
{
    [SerializeField] private PathCreator _path;

    private void Awake()
    {
        _path = GetComponent<PathCreator>();
    }

    public void GeneratePath(List<Vector3> points)
    {
       
        foreach (var point in points)
        {
            _path.bezierPath.AddSegmentToEnd(point);
        }
        _path.bezierPath.DeleteSegment(0);
        _path.bezierPath.DeleteSegment(0);
    }
}
