using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
  
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isYFrozen;
    public float freezeYTime = 1.0f; // Time for which the player's Y position is frozen

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3f);
        if (Input.GetKey(KeyCode.DownArrow) && (hit.collider != null))
        {
            Debug.Log("lol");
            isYFrozen = true;
            if (freezeYTime > 0)
            {
                freezeYTime -= Time.deltaTime;
            }
            else
            {
                isYFrozen = false;
                freezeYTime = 1.0f; // Reset freeze time
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!isYFrozen)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        } else
        {
            Debug.Log("frozen");
        }
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

