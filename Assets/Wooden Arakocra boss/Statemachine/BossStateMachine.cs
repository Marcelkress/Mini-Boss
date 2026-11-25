using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossStateMachine : MonoBehaviour
{
    [Header("References")] 
    public Animator animator;
    public PlayerStateMachine player;
    public SpriteRenderer sprite;
    
    [Header("Animation state names")] 
    public string idleAnim;
    public string attackOneAnim, attackTwoAnim;
    public string takeDamageAnim;

    [Header("Idle behavior")] 
    public float maxDistanceToPlayer;
    public float moveSpeed;

    [Header("Attack settings")] 
    [Range(1f, 2f)] public float attackWeight;
    public float maxTimeBetweenAttack, minTimeBetweenAttack;
    public float attackDistance;
    public float idleWindowIncrease = 2;
    [Range(0f, 1f)]public float chainAttackChance = 0.5f;

    [HideInInspector] public float timeUntilNextAttack;
    
    public BossBaseState currentState;
    private float idleTimer;

    private void Start()
    {   
        ChangeState(new BossIdleState(this));
        timeUntilNextAttack = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.BaseUpdate();
        currentState.Update();
        AttackCheck();
    }

    void AttackCheck()
    {
        idleTimer += Time.deltaTime;
        
        // If certain amount of time has passed AND the boss is within distance to the player
        if (idleTimer > timeUntilNextAttack && WithinDistanceToPlayer() && !player.health.isDead)
        {
            // Choose random attack 
            float rand = Random.value + 1;

            if (rand < attackWeight)
            {
                ChangeState(new BossAttackOneState(this));
            }
            else
            {
                ChangeState(new BossAttackTwoState(this));
            }

            idleTimer = 0;
            timeUntilNextAttack = Random.Range(minTimeBetweenAttack, maxTimeBetweenAttack);
        }
    }

    bool WithinDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position) < attackDistance;
    }
    
    public void ChangeState(BossBaseState newState)
    {
        if(currentState != null)
            currentState.Exit();
        
        currentState = newState;
        currentState.Enter();
    }
}
