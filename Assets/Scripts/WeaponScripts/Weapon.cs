using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 1f;
    protected float currCooldown = 0f;
    protected Animator animator;// Damange debounce so that the enemy doesn't take damage multiple times in one swing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void updateCooldowns()
    {
        if (currCooldown > 0)
        {
            currCooldown -= Time.deltaTime;
        }
    }

    public virtual void Attack()
    {
        if (currCooldown <= 0)
        {
            animator.SetTrigger("Attack");
            currCooldown = attackCooldown;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        updateCooldowns();
    }
}
