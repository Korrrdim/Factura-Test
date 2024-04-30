using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Game.Scripts.ComponentReference;
using System.Linq;
using DG.Tweening;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private ComponentReference _playerRef;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Renderer _renderer;
    [Space]
    [SerializeField] private HealthData _healthData;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _blood;
    private StateMachine _stateMachine;
    public int Damage => _damage;
    private Tween _colorTween;
    private Tween _shakeTween;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Init(_animator, _playerRef);
        _stateMachine.ChangeState(new IdleState(), "Idle");

        _healthBar.Init(_healthData.Health);
        _healthData.OnDeath += () => Death();
    }

    public void TakeDamage(int damage)
    {
        _healthData.Health -= damage;
        _healthBar.Invoke(_healthData.Health, 0.2f);

        Color startCol = _renderer.material.color;
        _colorTween?.Kill();
        _shakeTween = transform?.DOShakeScale(0.2f, 0.15f);
        _colorTween = _renderer?.material.DOColor(Color.white, 0.15f)
             .SetEase(Ease.OutElastic)
             .OnComplete(() => _renderer.material.DOColor(startCol, 0.05f));
    }

    private void FixedUpdate()
    {
        _stateMachine.UpdateMachine(_rb);

        if (Vector3.Distance(transform.position, _playerRef.Reference.transform.position) <= 15f)
            _stateMachine.ChangeState(new RunState(), "Run");
        else if (Vector3.Distance(transform.position, _playerRef.Reference.transform.position) <= 25f)
            _stateMachine.ChangeState(new AgroState(), "Idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(bullet.Damage);
            bullet.gameObject.SetActive(false);
        }
        if (collision.transform.TryGetComponent(out Player player))
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(_blood, transform.position, Quaternion.identity).Play();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _colorTween?.Kill();
        _shakeTween?.Kill();
    }
}
