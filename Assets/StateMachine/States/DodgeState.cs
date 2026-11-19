using UnityEngine;

public class DodgeState : BaseState
{
    public DodgeState(PlayerStateMachine p) : base(p) { }
    
    private float timer;
    private int dir;
    
    public override void Enter()
    {
        //Debug.Log("Enter Dodge!");
        if (player.dodgeCooldownTimer < player.dodgeCooldown)
        {
            // reject enter
            base.DefaultStateExitCheck();
            return;
        }
        
        player.input.ConsumeDodge();
        
        //player.animator.SetTrigger("Dodge");
        player.animator.Play(player.dodgeAnim);
        
        dir = player.facingLeft ? -1 : 1;

        player.dodgeCooldownTimer = 0;
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > player.dodgeDuration)
        {
            base.DefaultStateExitCheck();
            return;
        }
        
        Vector2 newPos = new Vector2(player.transform.position.x + (player.dodgeSpeed * dir) * Time.deltaTime, 
            player.transform.position.y) ;
        
        player.rb.MovePosition(newPos);
    }
    
    public override void Exit()
    {
        
    }
}
