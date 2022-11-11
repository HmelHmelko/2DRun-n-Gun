
public class PlayerGlidingState : PlayerAbilityState
{
    public PlayerGlidingState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        isAbilityDone = true;
    }
}
