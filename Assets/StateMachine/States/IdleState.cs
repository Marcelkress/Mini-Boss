using UnityEngine;
public class IdleState : BaseState
{
    public IdleState(PlayerStateMachine p) : base(p) { }
    
    public override void Enter()
    {
        Debug.Log("Enter Idle!");
    }

    public override void Update()
    {
        if (!Mathf.Approximately(player.input.move, 0))
        {
            player.ChangeState(new MoveState(player));
            return;
        }

        if (player.input.dodgePressed)
        {
            player.ChangeState(new DodgeState(player));
            return;
        }

        if (player.input.lightAttackPressed)
        {
            player.ChangeState(new LightAttackState(player));
            return;
        }
    }

    public override void Exit()
    {
        // Exit state logic
        Debug.Log("Exit Idle!");
    }
}