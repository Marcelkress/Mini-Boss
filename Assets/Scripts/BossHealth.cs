using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public float healthAnimSpeed = .8f;
    
    [Header("References")] 
    public BossStateMachine bossStateMachine;
    public Slider healthSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateUI();

        if (bossStateMachine.currentState is not BossAttackOneState && bossStateMachine.currentState is not BossAttackTwoState)
        {
            bossStateMachine.ChangeState(new BossHurtState(bossStateMachine));
        }
        
    }

    void UpdateUI()
    {
        healthSlider.DOValue(currentHealth, healthAnimSpeed);
    }
}
