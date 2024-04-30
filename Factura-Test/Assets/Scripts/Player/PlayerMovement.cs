using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerModule
{
    public Player Owner { get; set; }
    [SerializeField] private float _speed;
    private float _distanceTravelled;
    private bool _isMoving = false;


    public void Init()
    {
        Owner.OnFinish.OnEvent += Moving;
        Owner.OnDeath.OnEvent += Moving;
        Owner.OnMove.OnEvent += Moving;
    }

    private void Moving(bool value) => _isMoving = value;

    private void Update()
    {
        if (!_isMoving)
            return;

        _distanceTravelled += _speed * Time.deltaTime;
        transform.position = Owner.Path.path.GetPointAtDistance(_distanceTravelled, PathCreation.EndOfPathInstruction.Stop);
        transform.rotation = Owner.Path.path.GetRotationAtDistance(_distanceTravelled, PathCreation.EndOfPathInstruction.Stop);

        if(Owner.Path.path.GetClosestTimeOnPath(transform.position) >= 1f)
        {
            Owner.OnFinish?.Invoke(false);
        }
    }

    private void OnDisable()
    {
        Owner.OnFinish.OnEvent -= Moving;
        Owner.OnDeath.OnEvent -= Moving;
    }
}
