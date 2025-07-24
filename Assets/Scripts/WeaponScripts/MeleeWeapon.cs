using UnityEngine;

public class MeleeWeapon : Weapon
{
    public GameObject trailEffect;
    protected bool inSwing = false;
    protected bool canDamage = true; // Damange debounce so that the enemy doesn't take damage multiple times in one swing

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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

    public override void Attack()
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
}
