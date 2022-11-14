using UnityEngine;

public class PlayerState 
{
    protected Player _player;
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerData _playerData;
    private string animBoolName;
    //Otschet vremeni so starta lubogo newState
    protected float startTime;


    protected bool isAnimationFinished;
    protected bool isExitingState;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this._player = player;
        this._playerStateMachine = playerStateMachine;
        this._playerData = playerData;
        this.animBoolName = animBoolName;
    }   

    public virtual void Enter()
    {
        DoCheck();
        _player.animator.SetBool(animBoolName, true);
        startTime = Time.time;

        isAnimationFinished = false;
        isExitingState = false;

        Debug.Log(animBoolName);
    }

    public virtual void DoCheck()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }


    public virtual void Exit()
    {
        _player.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
