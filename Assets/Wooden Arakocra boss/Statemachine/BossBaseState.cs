using UnityEngine;

public class BossBaseState
{
    protected readonly BossStateMachine boss;
    
    private AnimatorClipInfo[] info;
    private float timer = 0;
    private bool exitOnAnimationFinish;

    
    protected BossBaseState(BossStateMachine boss)
    {
        this.boss = boss;
    }

    public virtual void Enter()
    {
        
    }

    public void BaseUpdate()
    {
        if (exitOnAnimationFinish)
        {
            timer += Time.deltaTime;
            info = boss.animator.GetCurrentAnimatorClipInfo(0);

            if (timer > info[0].clip.length)
            {
                boss.ChangeState(new BossIdleState(boss));
            }
        }
    }

    public virtual void Update()
    {
        
    }
    
    public virtual void Exit()
    {
        
    }

    protected void ExitOnAnimationFinish()
    {
        exitOnAnimationFinish = true;
    }
}
