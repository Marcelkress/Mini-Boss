using DG.Tweening;
using UnityEngine;

public class BossHurtState : BossBaseState
{
    public BossHurtState(BossStateMachine b) : base(b) { }

    private Tween moveTween;

    public override void Enter()
    {
        boss.animator.Play(boss.takeDamageAnim);
        ExitOnAnimationFinish();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
