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

    public virtual void Exit()
    {
        // Exit idle state logic
    }
}
