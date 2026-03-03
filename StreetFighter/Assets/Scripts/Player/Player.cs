using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int facingDirection = 1;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask attackLayerMask;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Inputs")]
    [SerializeField] private float moveInput;

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        Jump();

        AllAttacks();
        
        HandleAnimations();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * speed,rb.linearVelocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void AllAttacks()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Attack();
            MainAttack();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            FlyAttack();
            MainAttack();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            DiveAttack();
            MainAttack();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Kick();
            MainAttack();
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0 && isGrounded;

        animator.SetBool("isIdling", !isMoving && isGrounded);
        animator.SetBool("isRunning", isMoving && isGrounded);
        animator.SetBool("isJumping",rb.linearVelocity.y > 0);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void FlyAttack()
    {
        animator.SetTrigger("FlyKick");
    }

    void DiveAttack()
    {
        animator.SetTrigger("DiveKick");
    }

    void Kick()
    {
        animator.SetTrigger("Kick");
    }

    void MainAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayerMask);

        foreach(Collider2D hit in hitEnemies)
        {
            Debug.Log("Enemy Hit");
        }
    }
}