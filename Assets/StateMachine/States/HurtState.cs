using UnityEngine;

public class HurtState : BaseState
{
    public HurtState(PlayerStateMachine p) : base(p) { }
    
    public override void Enter()
    {
        player.animator.Play(player.hurtAnim);
        ExitOnAnimationFinish();
    }

    public override void Update()
    {
        if (player.health.isDead)
        {
            //Does not leave state. State machine restarts with game
            player.animator.Play(player.deathAnim);
            return;
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
