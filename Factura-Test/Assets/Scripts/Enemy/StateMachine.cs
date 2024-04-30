using Game.Scripts.ComponentReference;
using UnityEngine;

public class StateMachine
{
    private ComponentReference _playerRef;
    private DefaultState _state;
    private Animator _animator;

    public void Init(Animator animator, ComponentReference playerRef)
    {
        _animator = animator;
        _playerRef = playerRef;
    }

    public void ChangeState(DefaultState state, string animation = "none")
    {
        if (_state == state || _state == new DeadState())
            return;
        _state = state;

        if (animation == "none")
            return;
        _animator.Play(animation);
    }

    public void UpdateMachine(Rigidbody RB)
    {
        _state.Move(RB, _playerRef.Reference.transform);
        _state.Rotate(RB, _playerRef.Reference.transform);
    }
}

public class DefaultState
{
    public virtual void Move(Rigidbody RB, Transform target)
    {

    }

    public virtual void Rotate(Rigidbody RB, Transform target)
    {

    }
}

public class IdleState : DefaultState
{
}

public class DeadState : DefaultState
{
}

public class AgroState : DefaultState
{
    public override void Rotate(Rigidbody RB, Transform target)
    {
        Vector3 velocity = target.position - RB.position;
        Quaternion lookRotation = Quaternion.LookRotation(velocity, Vector3.up);
        RB.transform.rotation = Quaternion.Lerp(RB.rotation, lookRotation, Time.fixedDeltaTime * 5f);
    }
}


public class RunState : DefaultState
{
    public override void Move(Rigidbody RB, Transform target)
    {

        Vector3 movementDirection = target.position - RB.position;

        movementDirection = movementDirection.magnitude > 1 ? movementDirection.normalized : movementDirection;

        RB.AddForce(movementDirection.normalized * 1000f);
    }

    public override void Rotate(Rigidbody RB, Transform target)
    {
        Vector3 velocity = target.position - RB.position;
        Quaternion lookRotation = Quaternion.LookRotation(velocity, Vector3.up);
        RB.transform.rotation = Quaternion.Lerp(RB.rotation, lookRotation, Time.fixedDeltaTime * 5f);
    }
}