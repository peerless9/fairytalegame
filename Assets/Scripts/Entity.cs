using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar; // Assuming you have a HealthBar script to manage health UI

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void updateBar()
    {
        if (healthBar == null)
        {
            return;
        }
        healthBar.updateBar((float)currentHealth / maxHealth);
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateBar();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        updateBar();
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
