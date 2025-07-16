using UnityEngine;
using System.Collections;

public class Goose : Enemy
{
    [Header("Chase Stats")]
    public float chaseDuration = 5f;
    public float dashForce = 200f;
    public float dashTimeLength = 1f;
    public float chaseRest = 1f;

    [Header("Egg Throw Stats")]
    public float eggThrowForce = 100f;
    public float eggThrowRest = 1f;
    public GameObject eggPrefab;
    public int eggCount = 3;
    public int eggDamage = 20;
    public float eggThrowDuration = 2f;

    [Header("Spin Stats")]
    public float spinDuration = 3f;
    public float spinRest = 2.5f;
    public int feathersPerPulse = 5;
    public GameObject featherPrefab;
    public float featherSpeed = 10f;
    public int featherDamage = 10;
    public int pulses = 6;
    private bool currentlyAttacking = false;
    private bool canChase = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        healthBar = GameObject.FindGameObjectWithTag("BossBar").GetComponent<HealthBar>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target == null) return;
        if (canChase)
        {
            ChaseTarget();
        }
        else
        {
            FaceTarget();
        }
        if (currentlyAttacking) return;  

        //Attacking
        int move = Random.Range(1, 4); //1 = Chase, 2 = Throw, 3 = Spin
        if (move == 1)
        {
            StartCoroutine(StartCharge());
        }
        else if (move == 2)
        {
            StartCoroutine(StartEggThrow());
        }
        else if (move == 3)
        {
            StartCoroutine(StartSpin());
        }
    }

    private IEnumerator StartCharge()
    {
        currentlyAttacking = true;
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(chaseDuration);
        animator.SetBool("Charging", false);
        yield return new WaitForSeconds(chaseRest);
        currentlyAttacking = false;
    }

    public IEnumerator Dash() // Dash method is called by animation event
    {
        print("Goose is dashing!");
        rb.AddForce((target.position - transform.position).normalized * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTimeLength);
        rb.linearVelocity = Vector2.zero; // Stop the goose after dashing
    }

    private IEnumerator StartEggThrow()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement during egg throw
        currentlyAttacking = true;
        canChase = false;
        animator.SetTrigger("EggThrow");
        yield return new WaitForSeconds(eggThrowDuration);
        yield return new WaitForSeconds(eggThrowRest);
        currentlyAttacking = false;
        canChase = true;
    }

    public IEnumerator ShootEggs()
    {
        for (int i = 0; i < eggCount; i++)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.Euler(0,0,angle));
            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            eggRb.AddForce(direction * eggThrowForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(eggThrowDuration / eggCount);
        }
    }

    private IEnumerator StartSpin()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement during spin
        currentlyAttacking = true;
        canChase = false;
        animator.SetBool("Spinning", true);
        for (int i = 0; i < pulses; i++)
        {
            ShootFeathers();
            yield return new WaitForSeconds(spinDuration / pulses);
        }
        animator.SetBool("Spinning", false);
        yield return new WaitForSeconds(spinRest);
        currentlyAttacking = false;
        canChase = true;
    }

    public void ShootFeathers()
    {
        for (int i = 0; i < feathersPerPulse; i++)
        {
            Vector2 direction = (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f))).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            GameObject feather = Instantiate(featherPrefab, transform.position, Quaternion.Euler(0,0,angle));
            Rigidbody2D featherRb = feather.GetComponent<Rigidbody2D>();
            
            featherRb.AddForce(direction * featherSpeed, ForceMode2D.Impulse);
        }
    }
}
