using UnityEngine;
using System.Collections;

public class Goose : Enemy
{
    // Goose-specific stats
    [Header("Goose Stats")]
    public float chaseDuration = 5f;
    public float dashForce = 200f;
    public float dashTimeLength = 1f;
    public float dashCooldown = 3f;
    private float currCooldown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target != null)
        {
            animator.SetBool("Charging", true);
            ChaseTarget();
            UpdateCooldowns();
        }
    }

    // protected override void UpdateCooldowns()
    // {
    //     base.UpdateCooldowns();
    //     if (dashTimer > 0)
    //     {
    //         dashTimer -= Time.deltaTime;
    //     }
    // }

    public void TryDash()
    {

    }

    public IEnumerator Dash()
    {
        print("Goose is dashing!");
        rb.AddForce((target.position - transform.position).normalized * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTimeLength);
        rb.linearVelocity = Vector2.zero; // Stop the goose after dashing
    }
}
