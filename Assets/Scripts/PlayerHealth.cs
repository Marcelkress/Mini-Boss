using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public int healAmount;
    public int healFlaskAmount;
    public bool isDead;
    
    [Header("UI")] 
    public Slider healthSlider;
    public float sliderDuration = 0.5f;
    public GameObject[] flaskElements;
    public RetryUI retryUI;
    
    private int healFlasksLeft;
    public PlayerStateMachine player;
    public bool isInvincible;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDead = false;  
        player = GetComponent<PlayerStateMachine>();
        healFlasksLeft = healFlaskAmount;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateUI();
    }

    public bool HealFlaskCheck()
    {
        if (healFlasksLeft > 0)
            return true;

        return false;
    }

    public void Heal()
    {
        currentHealth += healAmount;
        healFlasksLeft--;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;
        
        // Changes the players state to hurt state. This overrides ANY state the player was in before.
        player.ChangeState(new HurtState(player));
        
        currentHealth -= damage;
        UpdateUI();
        
        if (currentHealth <= 0)
        {
            isDead = true;
            retryUI.EnableRetryUI();
        }
    }

    public void SetInvincibility(bool invincible)
    {
        isInvincible = invincible;
    }

    void UpdateUI()
    {
        healthSlider.DOValue(currentHealth, sliderDuration);

        for (int i = 0; i < flaskElements.Length; i++)
        {
            flaskElements[i].SetActive(i < healFlasksLeft);
        }
    }
}
