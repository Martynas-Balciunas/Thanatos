using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float moveSpeed = 5f;              // Horizontal movement speed
    [SerializeField] private float aliveJumpForce = 10f;        // Force for the initial jump in alive form
    [SerializeField] private float aliveDoubleJumpForce = 8f;   // Force for the double jump in alive form
    [SerializeField] private float ghostJumpForce = 7f;         // Force for the initial jump in ghost form
    [SerializeField] private float groundCheckRadius = 0.2f;    // Radius for ground check
    [SerializeField] private float ghostGravity;
    [SerializeField] private Transform groundCheck;             // Transform position for ground check
    [SerializeField] private LayerMask groundLayer;             // Layer mask for ground detection
    [SerializeField] private int maxHealth = 3;               // Maximum health of the player
    [SerializeField] private int keyCount = 0;
   
    private Rigidbody2D rb;                   // Reference to the Rigidbody2D component
    private bool isGrounded;                  // Whether the player is grounded
    private bool canDoubleJump;               // Whether the player can perform a double jump
    private int jumpCount = 0;                // Count of jumps performed
    private int currentHealth;                // Current health of the player
    private bool isAlive = true;              // Player state (alive)
    private bool isGhost = false;             // Player state (ghost)
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;            // Initialize health
        Instance = this;
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
        float moveInput = Input.GetAxis("Horizontal");
        // Reset double jump and jump count if grounded
        if (isGrounded && !isGhost)
        {
            canDoubleJump = true;
            jumpCount = 0;
        }
      

        // Handle horizontal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
       // Flip sprite based on movement direction
        if(rb.velocity.x  < 0)
        {
            FlipSprite(false);
        }
        else if (rb.velocity.x > 0)
        {
            FlipSprite(true);
        }
        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump(isGhost ? ghostJumpForce : aliveJumpForce);
            }
            else if (canDoubleJump && jumpCount < 1)
            {
                Jump(aliveDoubleJumpForce);
                canDoubleJump = false; // Disable double jump after usage
            }
        }
    }

    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazzard"))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("IronMaiden"))
        {
            if (!isGhost)
            {
                TakeDamage(1);
                ChangeForm("Ghost");
            }
            else
            {
                ChangeForm("Alive");
            }
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            KeyCollected();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            UseKey();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("MovingPlatform") && isGrounded)
        {
            transform.SetParent(collision.collider.transform,true);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isAlive && !isGhost)
        {
            currentHealth -= damage; 
            if (currentHealth <= 0)
            {
                UImanager.Instance.removeAllHearts();
                currentHealth = 0;
                GameOver();
            }
            for(int i = 0; i < damage; i++)
            {
                UImanager.Instance.removeHeartUI();
            }
            ChangeForm("Ghost");
        }
    }

    private void ChangeForm(string newState)
    {
        if (newState == "Ghost" && !isGhost)
        {
            isGhost = true;
            isAlive = true; // Keep alive state for game logic
            WorldManager.Instance.showGhostMap();
            rb.gravityScale = ghostGravity; // gravity change for ghost form
            animator.SetBool("isDead", true); ; // Update animation to show alive state

        }
        else if (newState == "Alive" && isGhost)
        {
            isGhost = false;
            isAlive = true;
            WorldManager.Instance.hideGhostMap();
            rb.gravityScale = 1; // Restore normal gravity
            animator.SetBool("isDead", false);// Update animation to show alive state
        }
    }

    private void GameOver()
    {
        isAlive = false;
        currentHealth = 0;
        // Play death effects & sound
        UImanager.Instance.removeAllHearts(); // ensures no hearts in case instant death
        UImanager.Instance.showLossMenu();
    }

    public void KeyCollected()
    {
        keyCount++;
        UImanager.Instance.updateKeyUI(keyCount);
    }
    public void UseKey()
    {
        if (keyCount <= 0)
        {
            return;
        }
        keyCount--;
        UImanager.Instance.updateKeyUI(keyCount);
    }
    private void gainHealth() // can be removed, or healing item added
    {
        if (currentHealth < maxHealth)
        {
            // increase health
            // play heal effect & sound
            UImanager.Instance.addHeartUI();
        }
    }
    void FlipSprite(bool forwards)
    {
        if (forwards)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;

        }    
    }
}
