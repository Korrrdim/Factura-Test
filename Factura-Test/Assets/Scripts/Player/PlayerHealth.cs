using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IDamageable, IPlayerModule
{
    [SerializeField] private HealthData _healthData;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ParticleSystem _carDestroy;
    private Tween _shakeTween;

    public Player Owner { get ; set ; }

    public void Init()
    {

    }

    private void Awake()
    {
        _healthBar.Init(_healthData.Health);
        _healthData.OnDeath += () => Death();
    }

    public void TakeDamage(int damage)
    {
        _healthData.Health -= damage;
        _healthBar.Invoke(_healthData.Health, 0.5f);

        _shakeTween = transform?.DOShakeScale(0.2f, 0.15f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            TakeDamage(enemy.Damage);
        }
    }

    private void Death()
    {
        Owner.OnDeath?.Invoke(false);
        _carDestroy.transform.parent = null;
        _carDestroy.Play();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _shakeTween?.Kill();
    }
}
