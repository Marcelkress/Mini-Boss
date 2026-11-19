using UnityEngine;

public abstract class BaseState
{
    protected readonly PlayerStateMachine player;
    
    private bool exitOnAnimationFinish;
    private AnimatorClipInfo[] info;
    private float timer = 0;
    
    protected BaseState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public virtual void Enter()
    {
        
    }
    
    public void BaseUpdate()
    {
        if (exitOnAnimationFinish)
        {
            timer += Time.deltaTime;
            info = player.animator.GetCurrentAnimatorClipInfo(0);

            if (timer > info[0].clip.length)
            {
                DefaultStateExitCheck();
            }
        }
    }

    public virtual void Update()
    {
        
    }
    
    public virtual void FixedUpdate()
    {
        
    }
    
    /// <summary>
    /// Exits state when the currently playing animation finishes
    /// </summary>
    protected virtual void ExitOnAnimationFinish()
    {
        exitOnAnimationFinish = true;
    }
    
    /// <summary>
    /// Covers: Idle, Moving, Dodge, light attack, heavy attack
    /// </summary>
    protected void DefaultStateExitCheck()
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
        
        if (player.input.specialAttackPressed)
        {
            player.ChangeState(new SpecialAttackState(player));
            return;
        }

        if (player.input.healPressed)
        {
            player.ChangeState(new HealState(player));
            return;
        }
        
        // Need to check that Idle and move states do not constantly transition to themselves
        if (Mathf.Approximately(player.input.move, 0) && this is not IdleState)
        {
            player.ChangeState(new IdleState(player));
        }
        
        if(Mathf.Abs(player.input.move) > 0.1 && this is not MoveState)
        {
            player.ChangeState(new MoveState(player));
        }
    }
    
    public virtual void Exit()
    {
        // Exit idle state logic
    }
}