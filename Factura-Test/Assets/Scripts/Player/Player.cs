using System.Collections;
using System;
using System.Collections.Generic;
using Game.Scripts.ComponentReference;
using Game.Scripts.Events;
using PathCreation;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ComponentReference _joyRef;
    public Joystick Joy { get => _joyRef.GetReference<Joystick>(); }

    [SerializeField] ComponentReference _pathCreatorRef;
    public PathCreator Path { get => _pathCreatorRef.GetReference<PathCreator>(); }

    [Space]

    public EventBool OnMove;
    public EventBool OnDeath;
    public EventBool OnFinish;

    private List<IPlayerModule> _modules;

    private IEnumerator Start()
    {
        _modules = new List<IPlayerModule>();
        GetComponentsInChildren(_modules);

        foreach (var characterModule in _modules)
        {
            characterModule.Owner = this;
        }

        foreach (var characterModule in _modules)
        {
            characterModule.Init();
        }

        yield return null;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnMove?.Invoke(true);
            OnMove = null;
        }
    }

}

[Serializable]
public struct HealthData
{
    public Action OnDeath;

    [SerializeField] private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;

            if (_health <= 0)
                OnDeath?.Invoke();
        }
    }
}