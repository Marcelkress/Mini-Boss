using UnityEngine;

public class BossAttackTwoState : BossBaseState
{
    public BossAttackTwoState(BossStateMachine b) : base(b) { }

    public override void Enter() 
    { 
        boss.animator.Play(boss.attackTwoAnim); 
        ExitOnAnimationFinish();
    }

    public override void Update() 
    {
        
    }

    public override void Exit()
    {
        boss.timeUntilNextAttack += boss.idleWindowIncrease;
    }
        
}
