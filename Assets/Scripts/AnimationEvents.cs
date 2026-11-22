using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public enum Attacktype
    {
        normal,
        extended
    }
    
    private PlayerHealth health;
    
    [SerializeField] private Transform attackOriginPoint, attackOriginPointExteneded;
    [SerializeField] private float attackRadius, attackRadiusExtended;
    
    public LayerMask bossLayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
    }
    
    public void Heal()
    {
        health.Heal();
    }
    
    public void Attack(Attacktype type)
    {
        Collider2D col;
        switch (type)
        {
            // Choose attack range based on specified type
            case Attacktype.extended:
                col = Physics2D.OverlapCircle(attackOriginPointExteneded.position, attackRadiusExtended, bossLayer);
                break;
            case Attacktype.normal:
                col = Physics2D.OverlapCircle(attackOriginPoint.position, attackRadius, bossLayer);
                break;
            default:
                col = Physics2D.OverlapCircle(attackOriginPoint.position, attackRadius, bossLayer);
                break;
        }

        // Choose attack damage based on which state the player is in
        if (col != null)
        {
            if(health.player.GetCurrentStateType() is LightAttackState)
                col.GetComponent<BossHealth>().TakeDamage(health.player.lightAttackDamage);
            else if (health.player.GetCurrentStateType() is HeavyAttackState)
                col.GetComponent<BossHealth>().TakeDamage(health.player.heavyAttackDamage);
            else
                col.GetComponent<BossHealth>().TakeDamage(health.player.specialAttackDamage);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOriginPoint.position, attackRadius);
        Gizmos.DrawWireSphere(attackOriginPointExteneded.position, attackRadiusExtended);
    }
}
