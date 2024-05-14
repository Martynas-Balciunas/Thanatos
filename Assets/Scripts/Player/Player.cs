using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float aliveJumpForce = 10f;
    [SerializeField] private float aliveDoubleJumpForce = 8f;
    [SerializeField] private float ghostJumpForce = 7f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float ghostGravity;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int keyCount = 0;
    [SerializeField] private LayerMask ghostCollisionLayer;
    [SerializeField] private AudioSource[] soundFX;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private int jumpCount = 0;
    private int currentHealth;
    private bool isAlive = true;
    private bool isGhost = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform originalParent;

    private AudioSource jumpSound;
    private AudioSource walkSound;
    private AudioSource collectItem1;
    private AudioSource collectItem2;
    private AudioSource openDoor;
    private AudioSource deathSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        Instance = this;
        soundFX = GetComponents<AudioSource>();

        foreach (AudioSource audioClip in soundFX)
        {
            if (audioClip.clip.name == "Jump")
            {
                jumpSound = audioClip;
            }
            else if (audioClip.clip.name == "walk")
            {
                walkSound = audioClip;
                walkSound.Play();
                walkSound.loop = true;
                walkSound.Pause();
            }
            else if (audioClip.clip.name == "collectItem1")
            {
                collectItem1 = audioClip;
            }
            else if (audioClip.clip.name == "collectItem2")
            {
                collectItem2 = audioClip;
            }
            else if(audioClip.clip.name == "door")
            {
                openDoor = audioClip;
            }
            else if (audioClip.clip.name == "death")
            {
                deathSound = audioClip;
            }
        }

        originalParent = transform.parent;
    }

    private void Update()
    {
        UImanager.Instance.updateKeyUI(keyCount);

        float moveInput = Input.GetAxis("Horizontal");

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset double jump and jump count if grounded
        if (isGrounded && !isGhost)
        {
            canDoubleJump = true;
            jumpCount = 0;
            animator.SetBool("isGrounded", true); // Update animation
        }
        else if (isGrounded && isGhost)
        {
            animator.SetBool("isGrounded", true); // Update animation

        }
        else
        {
            animator.SetBool("isGrounded", false); // Update animation
        }

        // Handle horizontal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            animator.SetBool("isWalking", true); // Walking animation when moving

            if( (isGrounded) && (isGhost==false))
            {
                //walkSound.Play();
                walkSound.UnPause();
            }
            else
            {
                walkSound.Pause();
            }
        }
        else
        {
            animator.SetBool("isWalking", false); // Idle when still
            walkSound.Pause();
        }

        // Flip sprite based on movement direction
        if (rb.velocity.x < 0)
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
                animator.SetTrigger("Jump"); // Update animation
            }
            else if (canDoubleJump && jumpCount < 1 )
            {
                Jump(aliveDoubleJumpForce);
                canDoubleJump = false; // Disable double jump after usage
                animator.SetTrigger("Jump"); // Update animation
            }
        }
    }

    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        jumpCount++;
        jumpSound.Play();
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
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.collider.transform, true);
        }
        if (collision.gameObject.CompareTag("Door") && !isGhost)
        {
            if(keyCount > 0)
            {
                UseKey();
                openDoor.Play();
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("InstantDeath"))
        {
            TakeDamage(currentHealth);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.collider.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(originalParent, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("victoryDoor") && !isGhost)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex + 1);
        }
        if (collision.gameObject.CompareTag("Hazzard"))
        {
            if (!isGhost)
            {
                TakeDamage(1);
                ChangeForm("Ghost");
            }
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            KeyCollected();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("medKit") && !isGhost)
        {
            gainHealth();
            Destroy(collision.gameObject);
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
            for (int i = 0; i < damage; i++)
            {
                UImanager.Instance.removeHeartUI();
            }
            ChangeForm("Ghost");
        }
    }

    private void ChangeForm(string newState)
    {
        int groundLayerIndex = LayerMask.NameToLayer("Ground");
        int ghostCollisionLayerIndex = LayerMask.NameToLayer("GhostCollision");

        if (groundLayerIndex == -1 || ghostCollisionLayerIndex == -1)
        {
            Debug.LogError("Layer names not correctly set up. Please check 'Ground' and 'GhostCollision' layers.");
            return;
        }

        if (newState == "Ghost" && !isGhost)
        {
            isGhost = true;
            isAlive = true; // Keep alive state for game logic
            WorldManager.Instance.showGhostMap();
            rb.gravityScale = ghostGravity; // gravity change for ghost form
            animator.SetBool("isDead", true); // Update animation to show ghost state

            // Ignore collisions with ground layer
            // Physics2D.IgnoreLayerCollision(gameObject.layer, groundLayerIndex, true);

            // Enable collisions with ghost collision layer
            Physics2D.IgnoreLayerCollision(gameObject.layer, ghostCollisionLayerIndex, false);
        }
        else if (newState == "Alive" && isGhost)
        {
            isGhost = false;
            isAlive = true;
            WorldManager.Instance.hideGhostMap();
            rb.gravityScale = 1; // Restore normal gravity
            animator.SetBool("isDead", false); // Update animation to show alive state
            animator.SetBool("isWalking", false); // Ensure walking is off initially

            // Restore collisions with ground layer
            Physics2D.IgnoreLayerCollision(gameObject.layer, groundLayerIndex, false);

            // Disable collisions with ghost collision layer
            Physics2D.IgnoreLayerCollision(gameObject.layer, ghostCollisionLayerIndex, true);
        }
    }

    private void GameOver()
    {
        isAlive = false;
        currentHealth = 0;
        // Play death effects & sound
        UImanager.Instance.removeAllHearts(); // ensures no hearts in case instant death
        deathSound.Play();
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
            currentHealth += 1;
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
