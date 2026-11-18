using UnityEngine;

public class DodgeState : BaseState
{
    public DodgeState(PlayerStateMachine p) : base(p) { }
    
    private float timer;
    
    public override void Enter()
    {
        Debug.Log("Enter Dodge!");

        if (player.dodgeCooldownTimer < player.dodgeCooldown)
        {
            // reject enter
            player.ChangeState(new IdleState(player));
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
            if(Mathf.Approximately(player.input.move, 0))
                player.ChangeState(new IdleState(player));
            else
                player.ChangeState(new MoveState(player));
            
            return;
        }

        int dir = player.facingLeft ? -1 : 1;
        
        Vector2 newPos = new Vector2(player.transform.position.x + (player.dodgeSpeed * dir) * Time.deltaTime, 
            player.transform.position.y) ;
        
        player.rb.MovePosition(newPos);
    }

    public override void Exit()
    {
        
    }
}
