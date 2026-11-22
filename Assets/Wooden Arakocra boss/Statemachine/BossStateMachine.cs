using System;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    [Header("References")] 
    public Animator animator;
    public GameObject player;
    public SpriteRenderer sprite;
    
    [Header("Animation state names")] 
    public string idleAnim;
    public string attackOneAnim, attackTwoAnim;
    public string takeDamageAnim;

    [Header("Idle behavior")] 
    public float maxDistanceToPlayer;
    public float moveSpeed;
    
    private BossBaseState currentState;

    private void Start()
    {
        ChangeState(new BossIdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.BaseUpdate();
        currentState.Update();
    }

    public void ChangeState(BossBaseState newState)
    {
        if(currentState != null)
            currentState.Exit();
        
        currentState = newState;
        currentState.Enter();
    }
}
