using UnityEngine;

public class HealState : BaseState
{
    public HealState(PlayerStateMachine p) : base(p) { }
    
    public override void Enter()
    {
        player.input.ConsumeHeal();
        
        if(player.health.HealFlaskCheck())
            player.animator.Play(player.healAnim);
        else
            player.animator.Play(player.healFailAnim);
        
        ExitOnAnimationFinish();
    }
    

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
