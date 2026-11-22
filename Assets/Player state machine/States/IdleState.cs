using UnityEngine;
public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine p) : base(p) { }
    
    public override void Enter()
    {
        player.animator.Play(player.idleAnim);
    }

    public override void Update()
    {
        DefaultStateExitCheck();
    }

    public override void Exit()
    {
        // Exit state logic
        //Debug.Log("Exit Idle!");
    }
}