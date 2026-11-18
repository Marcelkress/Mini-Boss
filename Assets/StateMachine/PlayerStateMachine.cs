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
    public float attackStringTime;
    public float lightAttackDuration;
    
    private BaseState currentState;
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

        dodgeCooldownTimer += Time.deltaTime;
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
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
    public void OnLightAttack()
    {
        input.lightAttackPressed = true;
    }

    #endregion
}

public class PlayerInput
{
    public float move;
    public bool dodgePressed;
    public bool lightAttackPressed;

    public bool ConsumeLightAttack()
    {
        bool prev = lightAttackPressed;
        lightAttackPressed = false;
        return prev;
    }

    public bool ConsumeDodge()
    {
        bool prev = dodgePressed;
        dodgePressed = false;
        return prev;
    }
}