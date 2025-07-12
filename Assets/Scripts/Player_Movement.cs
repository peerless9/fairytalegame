using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{

    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 moveInput;
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
                //spriteRenderer.flipX = moveInput.x < 0;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

}
