using DG.Tweening;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    public BossIdleState(BossStateMachine b) : base(b) { }

    private Tween moveTween;

    public override void Enter()
    {
        boss.animator.Play(boss.idleAnim);
    }

    public override void Update()
    {
        LookAtPlayer();
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (Vector2.Distance(boss.transform.position, boss.player.transform.position)
            > boss.maxDistanceToPlayer && (moveTween == null || moveTween.IsActive()))
        {
            int dir = (int)Mathf.Sign(boss.transform.position.x - boss.player.transform.position.x);
            
            Vector2 newPos = new Vector2(boss.player.transform.position.x + (boss.maxDistanceToPlayer * dir), 
                                        boss.transform.position.y);

            moveTween = boss.transform.DOMove(newPos, boss.moveSpeed).OnComplete(() => moveTween = null)
                .OnKill(() => moveTween = null);
        }
    }

    private void LookAtPlayer()
    {
        if (Mathf.Sign(boss.transform.position.x - boss.player.transform.position.x) > 0)
        {
            boss.sprite.flipX = true;
        }
        else
        {
            boss.sprite.flipX = false;
        }
    }

    public override void Exit()
    {
        
    }
}
