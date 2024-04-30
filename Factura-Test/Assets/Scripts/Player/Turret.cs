using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Pool;
using UnityEngine;

public class Turret : MonoBehaviour, IPlayerModule
{
    public Player Owner { get; set; }
    [SerializeField] private float _rotationSpeed;
    [Space]
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private ParticleSystem _shoot;
    private bool _isShooting = false;

    private float _timer;

    public void Init()
    {
        Owner.OnFinish.OnEvent += IsShooting;
        Owner.OnDeath.OnEvent += IsShooting;
        Owner.OnMove.OnEvent += IsShooting;
        _timer = _delay;
    }

    private void IsShooting(bool value) => _isShooting = value;

    private void FixedUpdate()
    {
        Rotating();
        TryShoot();
    }

    private void TryShoot()
    {
        if (!_isShooting) return;

        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
        {
            _timer = _delay;
            var bullet = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.Instance.AllTags[0], _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Release();
            _shoot.Play();
        }
    }

    private void Rotating()
    {
        Vector3 velocity2D = new Vector3(Owner.Joy.Horizontal, 0, Owner.Joy.Vertical);
        if (velocity2D.magnitude == 0) return;
        Quaternion lookRotation = Quaternion.LookRotation(velocity2D, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        Owner.OnFinish.OnEvent -= IsShooting;
        Owner.OnDeath.OnEvent -= IsShooting;
    }
}
