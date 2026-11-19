using UnityEngine;

public class DodgeState : BaseState
{
    public DodgeState(PlayerStateMachine p) : base(p) { }
    
    private int dir;
    
    public override void Enter()
    {
        player.input.ConsumeDodge();
        
        //Debug.Log("Enter Dodge!");
        if (player.dodgeCooldownTimer < player.dodgeCooldown)
        {
            // reject enter
            DefaultStateExitCheck();
            return;
        }

        // Player cant take damage
        player.health.SetInvincibility(true);
        player.animator.Play(player.dodgeAnim);
        
        // Exit this state when the animation is finished
        ExitOnAnimationFinish();
        
        dir = player.facingLeft ? -1 : 1;
        player.dodgeCooldownTimer = 0;
    }

    public override void FixedUpdate()
    {
        Vector2 newPos = new Vector2(player.transform.position.x + (player.dodgeSpeed * dir) * Time.deltaTime, 
            player.transform.position.y) ;
        
        player.rb.MovePosition(newPos);
    }
    
    public override void Exit()
    {
        player.health.SetInvincibility(false);
    }
}
