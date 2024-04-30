using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _power;
    [SerializeField] private int _damage;
    private Rigidbody RB;

    public int Damage => _damage;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void Release()
    {
        RB.AddForce(transform.forward * _power, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
