using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerHealth health;

    [Header("Input settings")] 
    [SerializeField] private float inputBuffer;
    
    [Header("Movement")] 
    public float moveSpeed;
    [HideInInspector] public bool facingLeft;
    
    [Header("Dodge")] 
    public float dodgeSpeed = 2;
    public float dodgeCooldown = 1f;
    [HideInInspector] public float dodgeCooldownTimer;

    [Header("Attack")] 
    public float endOfAttackInputBuffer;
    public int lightAttackDamage, heavyAttackDamage, specialAttackDamage;
    
    [Header("Animation state names")] 
    public string idleAnim;
    public string movingAnim, dodgeAnim, healAnim, healFailAnim, hurtAnim, deathAnim;
    public string specialAttackAnim;
    public string lightAttackOneAnim, lightAttackTwoAnim, lightAttackThreeAnim;
    public string heavyAttackOneAnim, heavyAttackTwoAnim, heavyAttackThreeAnim;
    
    private PlayerBaseState currentState;
    private bool dirSwitchLock;
    public Input input = new();
    private float bufferTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dodgeCooldownTimer = 0;
        facingLeft = false;
        ChangeState(new IdleState(this));
    }

    public void ChangeState(PlayerBaseState newState)
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
        currentState.Update();
        currentState.BaseUpdate();
        InputBuffer();
    }

    private void InputBuffer()
    {
        // When a certain amount of time has passed since the last input, we want to clear it so we don't queue too many inputs
        if (input.newInput)
        {
            bufferTimer += Time.deltaTime;

            if (bufferTimer > inputBuffer)
            {
                input.ClearAll();
                bufferTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        SwitchFacingDirection();
        currentState.FixedUpdate();
    }

    public void SwitchFacingDirection()
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

    public PlayerBaseState GetCurrentStateType()
    {
        return currentState;
    }
    
    #region  InputMethods
    
    public void OnMove(InputValue value)
    {
        input.move = value.Get<float>();
    }

    public void OnDodge()
    {
        input.dodgePressed = true;
        input.newInput = true;
    }

    public void OnHeavyAttack()
    {
        input.heavyAttackPressed = true;
        input.newInput = true;
    }
    
    public void OnLightAttack()
    {
        input.lightAttackPressed = true;
        input.newInput = true;
    }

    public void OnHeal()
    {
        input.healPressed = true;
        input.newInput = true;
    }

    public void OnHurtTest()
    {
        PlayerHealth health = GetComponent<PlayerHealth>();
        health.TakeDamage(20);
    }

    public void OnSpecialAttack()
    {
        input.specialAttackPressed = true;
        input.newInput = true;
    }

    #endregion
}

public class Input
{
    public bool newInput;
    
    public float move;
    public bool dodgePressed;
    public bool lightAttackPressed;
    public bool heavyAttackPressed;
    public bool specialAttackPressed;
    public bool healPressed;

    public void ConsumeSpecialAttack()
    {
        specialAttackPressed = false;
    }
    
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

    public void ClearAll()
    {
        dodgePressed = lightAttackPressed = healPressed = heavyAttackPressed = specialAttackPressed = false;
    }
}