using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerHealth health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
    }

    public void Heal()
    {
        health.Heal();
    }
}
