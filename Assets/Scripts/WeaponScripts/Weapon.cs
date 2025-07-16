using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 1f;
    public GameObject trailEffect;
    protected float currCooldown = 0f;
    protected Animator animator;
    protected bool inSwing = false;
    protected bool canDamage = true; // Damange debounce so that the enemy doesn't take damage multiple times in one swing

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

    public void startSwing()
    {
        trailEffect.SetActive(true);
        inSwing = true;
    }

    public void endSwing()
    {
        trailEffect.SetActive(false);
        inSwing = false;
    }

    public virtual void Attack()
    {
        if (currCooldown <= 0)
        {
            canDamage = true; // Reset damage debounce
            animator.SetTrigger("Attack");
            currCooldown = attackCooldown;

        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.CompareTag("Enemy") && inSwing && canDamage)
        {
            canDamage = false; // Prevents multiple hits in one swing
            print("GET HIM");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        updateCooldowns();
    }
}
