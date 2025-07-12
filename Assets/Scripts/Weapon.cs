using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float attackCooldown = 1f;
    private float currCooldown = 0f;
    private Animator animator;
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
