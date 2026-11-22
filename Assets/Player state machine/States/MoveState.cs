using UnityEngine;

public class MoveState : PlayerBaseState
{
    public MoveState(PlayerStateMachine p) : base(p) { }

    public override void Enter()
    {
        //Debug.Log("Enter moving!");
        //player.animator.SetBool("Run", true);
        player.animator.Play(player.movingAnim);
    }

    public override void Update()
    {
        base.DefaultStateExitCheck();
    }

    public override void FixedUpdate()
    {
        Vector2 newPos = new Vector2(player.transform.position.x + Mathf.Sign(player.input.move) * player.moveSpeed * Time.deltaTime, player.transform.position.y) ;
        
        player.rb.MovePosition(newPos);
    }

    public override void Exit()
    { 
       // Debug.Log("Exit moving!");
        player.animator.SetBool("Run", false);
    }
}
