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

    /// <summary>
    /// Covers: Idle, Moving, Dodge, light attack, heavy attack
    /// </summary>
    public virtual void DefaultStateExitCheck()
    {
        
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
        
        if (player.input.heavyAttackPressed)
        {
            player.ChangeState(new HeavyAttackState(player));
            return;
        }

        if (player.input.healPressed)
        {
            player.ChangeState(new HealState(player));
            return;
        }
        
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