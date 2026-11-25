using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerAttackManager attackManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
        attackManager = GetComponentInParent<PlayerAttackManager>();
    }
    
    public void Heal()
    {
        health.Heal();
    }
    
    public void Attack(PlayerAttackManager.AttackID attackID)
    {
        attackManager.Attack(attackID);
    }
}
