using UnityEngine;

public class HurtState : PlayerBaseState
{
    public HurtState(PlayerStateMachine p) : base(p) { }

    private bool dead;
    
    public override void Enter()
    {
        player.animator.Play(player.hurtAnim);
        ExitOnAnimationFinish();
        dead = false;
    }

    public override void Update()
    {
        if (player.health.isDead && !dead)
        {
            dead = true;
            //Does not leave state. State machine restarts with game
            ExitOnAnimationFinish(false);
            player.animator.Play(player.deathAnim);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
