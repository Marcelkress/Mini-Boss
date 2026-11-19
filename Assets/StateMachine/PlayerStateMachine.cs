using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    
    [Header("Movement")] 
    public float moveSpeed;
    [HideInInspector] public bool facingLeft;
    
    [Header("Dodge")] 
    public float dodgeSpeed = 2;
    public float dodgeDuration = 1.5f;
    public float dodgeCooldown = 1f;
    [HideInInspector] public float dodgeCooldownTimer;

    [Header("Attacking")] 
   
    [Header("Animation state names")] 
    public string idleAnim;
    public string movingAnim, dodgeAnim, healAnim;
    public string lightAttackOneAnim, lightAttackTwoAnim, lightAttackThreeAnim;
    public string heavyAttackOneAnim, heavyAttackTwoAnim, heavyAttackThreeAnim;
    
    private BaseState currentState;
    private bool dirSwitchLock;
    public PlayerInput input = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dodgeCooldownTimer = 0;
        facingLeft = false;
        ChangeState(new IdleState(this));
    }

    public void ChangeState(BaseState newState)
    {
        // Exit the current state
        if(currentState != null)
            currentState.Exit();

        // Assign the new state and enter it
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        dodgeCooldownTimer += Time.deltaTime;
        SwitchFacingDirection();
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void SwitchFacingDirection()
    {
        if (dirSwitchLock)
            return;
        
        if (input.move > 0.1)
        {
            if (facingLeft)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            facingLeft = false;
            
        }
        else if (input.move < -0.1)
        { 
            if (!facingLeft)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            facingLeft = true;
        }
    }

    public void LockDirectionSwitch(bool lockVal)
    {
        dirSwitchLock = lockVal;
    }
    
    #region  InputMethods
    
    public void OnMove(InputValue value)
    {
        input.move = value.Get<float>();
    }

    public void OnDodge()
    {
        input.dodgePressed = true;
    }

    public void OnHeavyAttack()
    {
        input.heavyAttackPressed = true;
    }
    
    public void OnLightAttack()
    {
        input.lightAttackPressed = true;
    }

    public void OnHeal()
    {
        input.healPressed = true;
    }

    #endregion
}

public class PlayerInput
{
    public float move;
    public bool dodgePressed;
    public bool lightAttackPressed;
    public bool heavyAttackPressed;
    public bool healPressed;

    public void ConsumeHeal()
    {
        healPressed = false;
    }
    
    public bool ConsumeHeavyAttack()
    {
        bool prev = heavyAttackPressed;
        heavyAttackPressed = false;
        return prev;
    }

    public bool ConsumeLightAttack()
    {
        bool prev = lightAttackPressed;
        lightAttackPressed = false;
        return prev;
    }

    public void ConsumeDodge()
    {
        //bool prev = dodgePressed;
        dodgePressed = false;
        //return prev;
    }
}