using System.Collections;
using UnityEngine;

public class LightAttackState : PlayerBaseState
{
    public LightAttackState(PlayerStateMachine p) : base(p) { }

    private float attackStringTimer;
    private int attackStringStep;
    private bool attackQueued;
    private AnimatorClipInfo[] info;
    
    public override void Enter()
    {
        player.SwitchFacingDirection();
        player.LockDirectionSwitch(true);
        attackStringStep = 0;
        player.input.ConsumeLightAttack(); 
        StartStep();
    }

    public override void Update()
    {
        attackStringTimer += Time.deltaTime;
        
        if (player.input.ConsumeLightAttack())
            attackQueued = true;
        
        info = player.animator.GetCurrentAnimatorClipInfo(0);
        float clipLength = info[0].clip.length;
        
        if (attackStringTimer < clipLength)
            return;

        if (attackQueued && attackStringStep < 4)
        {
            StartStep();
            return;
        }
        
        bool lastHit = attackStringStep >= 4;

        if (lastHit || attackStringTimer >= clipLength + player.endOfAttackInputBuffer)
        {
            DefaultStateExitCheck();
        }
    }

    private void StartStep()
    {
        attackStringStep++;
        attackStringTimer = 0f;
        attackQueued = false;
        //player.animator.SetTrigger("LightAttack");

        switch (attackStringStep)
        {
            case 1:
                player.animator.Play(player.lightAttackOneAnim);
                break;
            case 2:
                player.animator.Play(player.lightAttackTwoAnim);
                break;
            case 3:
                player.animator.Play(player.lightAttackThreeAnim);
                break;
        }
    }
    

    public override void Exit()
    {
        player.LockDirectionSwitch(false);
        //Debug.Log("Exit light attack state!");
    }
}
