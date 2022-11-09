using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    float jumpRealesedDecreaseVelocity;
    private int numberOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        jumpRealesedDecreaseVelocity = playerData.jumpRealesedDecreaseVelocity;
        numberOfJumpsLeft = playerData.numberOfJumps;
    }
    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityY(_playerData.jumpVelocity);
        _player.airState.SetIsJumping();
        isAbilityDone = true;
        numberOfJumpsLeft--;
    }

    public bool CanJump()
    {
        if(numberOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetNumberOfJumps() => numberOfJumpsLeft = _playerData.numberOfJumps;

    public void DecreaseNumberOfJumpsLeft() => numberOfJumpsLeft--;

}
