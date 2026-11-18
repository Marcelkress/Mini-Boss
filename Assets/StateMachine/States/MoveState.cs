using UnityEngine;

public class MoveState : BaseState
{
    public MoveState(PlayerStateMachine p) : base(p) { }

    public override void Enter()
    {
        Debug.Log("Enter moving!");
    }

    public override void Update()
    {
        if (Mathf.Approximately(player.input.move, 0))
        {
            player.ChangeState(new IdleState(player));
            return;
        }
        
        if (player.input.dodgePressed)
        {
            player.ChangeState(new DodgeState(player));
            return;
        }
    }

    public override void FixedUpdate()
    {
        Vector2 newPos = new Vector2(player.transform.position.x + player.input.move * player.moveSpeed * Time.deltaTime, player.transform.position.y) ;
        
        player.rb.MovePosition(newPos);
    }

    public override void Exit()
    { 
        Debug.Log("Exit moving!");
    }
}
