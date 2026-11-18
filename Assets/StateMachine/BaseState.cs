using UnityEngine;

public abstract class BaseState
{
    protected readonly PlayerStateMachine player;

    protected BaseState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public virtual void Enter()
    {
        // Enter idle state logic
    }

    public virtual void Update()
    {
        // Idle state update logic
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void DefaultStateExit()
    {
        if(Mathf.Approximately(player.input.move, 0))
            player.ChangeState(new IdleState(player));
        else
            player.ChangeState(new MoveState(player));
    }

    public virtual void Exit()
    {
        // Exit idle state logic
    }
}