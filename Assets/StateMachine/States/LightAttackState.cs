using UnityEngine;

public class LightAttackState : BaseState
{
    public LightAttackState(PlayerStateMachine p) : base(p) { }

    private float attackStringTimer;
    private int attackStringStep;
    
    public override void Enter()
    {
        Attack();
        attackStringStep = 1;
    }

    public override void Update()
    {
        attackStringTimer += Time.deltaTime;

        if (attackStringTimer < player.attackStringTime && player.input.lightAttackPressed)
        {
            Attack();
        }
        else if (attackStringTimer > player.attackStringTime)
        {
            // Somehow wait for end of animation
            player.ChangeState(new IdleState(player));
        }
    }

    public override void FixedUpdate()
    {
        
    }

    private void Attack()
    {
        attackStringStep++;
        attackStringTimer = 0;
        Debug.Log("Deal damage");
        // Trigger animation
        
        // Somehow wait for end of animation
        if (attackStringStep >= 3)
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit light attack state!");
    }
}
