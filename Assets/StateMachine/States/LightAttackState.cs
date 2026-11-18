using System.Collections;
using UnityEngine;

public class LightAttackState : BaseState
{
    public LightAttackState(PlayerStateMachine p) : base(p) { }

    private float attackStringTimer;
    private int attackStringStep;
    private bool attackQueued;
    
    public override void Enter()
    {
        attackStringStep = 0;
        player.input.ConsumeLightAttack(); 
        StartStep();
    }

    public override void Update()
    {
        attackStringTimer += Time.deltaTime;
        
        if (player.input.ConsumeLightAttack())
            attackQueued = true;

        if (attackStringTimer < player.lightAttackDuration)
            return;

        if (attackQueued && attackStringStep < 4)
        {
            StartStep();
            return;
        }
        
        bool lastHit = attackStringStep >= 4;
        
        if (lastHit || attackStringTimer >= player.lightAttackDuration + player.attackStringTime)
            base.DefaultStateExit();
    }

    private void StartStep()
    {
        attackStringStep++;
        attackStringTimer = 0f;
        attackQueued = false;
        player.animator.SetTrigger("LightAttack");
    }
    

    public override void Exit()
    {
        Debug.Log("Exit light attack state!");
    }
}
