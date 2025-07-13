using UnityEngine;
using System.Collections;

public class Goose : Enemy
{
    [Header("Chase Stats")]
    public float chaseDuration = 5f;
    public float dashForce = 200f;
    public float dashTimeLength = 1f;

    [Header("Egg Throw Stats")]
    public float eggThrowForce = 100f;
    public GameObject eggPrefab;
    public int eggCount = 3;
    public int eggDamage = 20;
    public float eggThrowDuration = 2f;

    [Header("Spin Stats")]
    public float spinDuration = 3f;
    public int feathersPerPulse = 5;
    public GameObject featherPrefab;
    public float featherSpeed = 10f;
    public int featherDamage = 10;
    public int pulses = 6;
    private bool currentlyAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target == null) return;
        FaceTarget();
        if (currentlyAttacking) return;
        ChaseTarget();

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
        currentlyAttacking = true;
        animator.SetTrigger("EggThrow");
        yield return new WaitForSeconds(eggThrowDuration);
        currentlyAttacking = false;
    }

    private void ShootEggs()
    {
        for (int i = 0; i < eggCount; i++)
        {
            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();
            Vector2 direction = (target.position - transform.position).normalized;
            eggRb.AddForce(direction * eggThrowForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator StartSpin()
    {
        currentlyAttacking = true;
        animator.SetBool("Spinning", true);
        for (int i = 0; i < pulses; i++)
        {
            ShootFeathers();
            yield return new WaitForSeconds(spinDuration / pulses);
        }
        animator.SetBool("Spinning", false);
        currentlyAttacking = false;
    }

    public void ShootFeathers()
    {
        for (int i = 0; i < feathersPerPulse; i++)
        {
            GameObject feather = Instantiate(featherPrefab, transform.position, Quaternion.identity);
            Rigidbody2D featherRb = feather.GetComponent<Rigidbody2D>();
            Vector2 direction = (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f))).normalized;
            featherRb.AddForce(direction * featherSpeed, ForceMode2D.Impulse);
        }
    }
}
