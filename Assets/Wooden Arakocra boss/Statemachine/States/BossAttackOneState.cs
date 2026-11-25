using UnityEngine;

public class BossAttackOneState : BossBaseState
{
    public BossAttackOneState(BossStateMachine b) : base(b) { }

    private bool chainAttack;
    private AnimatorClipInfo[] info;
    private float timer = 0;
    private bool exitOnAnimationFinish;
    
    public override void Enter()
    {
        boss.animator.Play(boss.attackOneAnim);

        if (Random.value > boss.chainAttackChance)
        {
            ExitOnAnimationFinish();
            chainAttack = false;
        }
        else
        {
            chainAttack = true;
        }
    }

    public override void Update()
    {
        if (chainAttack)
        {
            timer += Time.deltaTime;
            info = boss.animator.GetCurrentAnimatorClipInfo(0);

            if (timer > info[0].clip.length)
            {
                boss.ChangeState(new BossAttackTwoState(boss));
            }
        }
    }

    public override void Exit()
    {

    }
}
