using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    [Header("Movement")] 
    public float moveSpeed;
    public bool facingLeft;
    
    [Header("Dodge")] 
    public float dodgeSpeed = 2;
    public float dodgeDuration = 1.5f;
    
    private BaseState currentState;
    public PlayerInput input;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            facingLeft = false;
        }
        else if (input.move < -0.1)
        { 
            facingLeft = true;
        }
            
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

        StartCoroutine(ResetDodge());
    }

    IEnumerator ResetDodge()
    {
        yield return new WaitForEndOfFrame();
        input.dodgePressed = false;
    }

    #endregion
}

public struct PlayerInput
{
    public float move;
    public bool dodgePressed;
}