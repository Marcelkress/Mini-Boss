using UnityEngine;

public class BossAnimEvents : MonoBehaviour
{
    public Transform attackOriginPoint;
    public float attackRadius;
    public LayerMask playerLayer;
    
    public void Attack(int damage)
    {
        Collider2D col = Physics2D.OverlapCircle(attackOriginPoint.position, attackRadius, playerLayer);
        
        if (col != null)
        {
            if(col.TryGetComponent(out PlayerHealth pl))
            {
                Debug.Log("Here");
                pl.TakeDamage(damage);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOriginPoint.position, attackRadius);
    }
}
