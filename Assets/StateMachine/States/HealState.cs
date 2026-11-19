using UnityEngine;

public class HealState : BaseState
{
    public HealState(PlayerStateMachine p) : base(p) { }

    private AnimatorClipInfo[] info;
    private float timer;
    
    public override void Enter()
    {
        player.animator.Play(player.healAnim);
        player.input.ConsumeHeal();
        timer = 0;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        info = player.animator.GetCurrentAnimatorClipInfo(0);

        if (timer > info[0].clip.length)
        {
            base.DefaultStateExitCheck();
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
