using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;

    protected int health;
    protected Transform target;
    protected Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogError("Player not found! Make sure there is a GameObject with the tag 'Player' in the scene.");
        }
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
