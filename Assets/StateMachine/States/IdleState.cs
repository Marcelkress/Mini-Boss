using UnityEngine;
public class IdleState : BaseState
{
    public IdleState(PlayerStateMachine p) : base(p) { }
    
    public override void Enter()
    {
        Debug.Log("Enter Idle!");
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