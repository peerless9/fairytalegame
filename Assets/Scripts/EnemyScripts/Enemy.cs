using UnityEngine;

public class Enemy : Entity
{
    //Stats for the enemy
    [Header("Enemy Stats")]
    public float speed = 2f;
    public float damage = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    protected float currCooldown = 0f;

    protected Transform target;
    protected Rigidbody2D rb;
    protected Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogError("Player not found! Make sure there is a GameObject with the tag 'Player' in the scene.");
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FaceTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected virtual void ChaseTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            if (rb.linearVelocity.sqrMagnitude < speed * speed)
            {
                rb.linearVelocity = direction * speed;
            }
            FaceTarget();
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void UpdateCooldowns()
    {
        if (currCooldown > 0)
        {
            currCooldown -= Time.deltaTime;
        }
    }
}