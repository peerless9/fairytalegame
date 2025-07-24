using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player_Movement : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [Header("Dash Settings")]
    public float dashForce = 50f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private Rigidbody2D rb;
    public GameObject dashEffectPrefab;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 moveInput;
    private float dashTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
            if (moveInput.x != 0)
            {
                transform.rotation = Quaternion.Euler(0, moveInput.x < 0 ? 180 : 0, 0);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (dashTimer < dashCooldown - dashDuration)
        {
            rb.linearVelocity = Vector2.zero; // Reset the timer if cooldown is over
        }
        if (rb.linearVelocity.sqrMagnitude <= moveSpeed * moveSpeed)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        UpdateTimers();
    }


    void FixedUpdate()
    {
        if (dashTimer > dashCooldown - dashDuration)
        {
            GameObject afterEffect = Instantiate(dashEffectPrefab, transform.position, transform.rotation);
            afterEffect.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            Destroy(afterEffect,0.2f);
        }
    }

    private void UpdateTimers()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        print("Dash input received");
        if (context.started && dashTimer <= 0f)
        {
            print("Dash initiated");
            Vector2 dashDirection = moveInput.normalized;
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
            dashTimer = dashCooldown;
        }
    }
}
