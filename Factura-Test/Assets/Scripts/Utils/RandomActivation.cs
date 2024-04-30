using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivation : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rareObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();

    private void Awake()
    {
        foreach (var item in _rareObjects)
        {
            if (Random.RandomRange(0, 100) <= 80)
                item.SetActive(false);
        }
        foreach (var item in _objects)
        {
            if (Random.RandomRange(0, 100) <= 35)
                item.SetActive(false);
        }
    }
}
