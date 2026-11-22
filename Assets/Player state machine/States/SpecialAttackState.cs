using UnityEngine;

public class SpecialAttackState : PlayerBaseState
{
    public SpecialAttackState(PlayerStateMachine p) : base(p) { }

    public override void Enter()
    {
        player.animator.Play(player.specialAttackAnim);
        player.input.ConsumeSpecialAttack();
        player.LockDirectionSwitch(true);
        ExitOnAnimationFinish();
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        player.LockDirectionSwitch(false);
    }
}
