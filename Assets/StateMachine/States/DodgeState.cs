using UnityEngine;

public class DodgeState : BaseState
{
    public DodgeState(PlayerStateMachine p) : base(p) { }
    
    private float timer;
    private int dir;
    
    public override void Enter()
    {
        Debug.Log("Enter Dodge!");

        player.input.ConsumeDodge();
        
        dir = player.facingLeft ? -1 : 1;

        if (player.dodgeCooldownTimer < player.dodgeCooldown)
        {
            // reject enter
            base.DefaultStateExit();
            return;
        }

        player.dodgeCooldownTimer = 0;
        player.animator.SetTrigger("Dodge");
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > player.dodgeDuration)
        {
            base.DefaultStateExit();
            
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
