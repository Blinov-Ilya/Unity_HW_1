using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float jumpForce = 10f;        
    private bool isGrounded = false;
    private bool isLeftWalled = false;
    private bool isRightWalled = false;
    private bool isDead = false;
    public LayerMask floorLayer;
    public LayerMask leftWallLayer;
    public LayerMask rightWallLayer;
    public LayerMask enemyLayer;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            var mask = (1 << collider.gameObject.layer);
            if ((mask & floorLayer) != 0)
            {
                isGrounded = true;
            }
            if ((mask & leftWallLayer) != 0)
            {
                isLeftWalled = true;
            }
            if ((mask & rightWallLayer) != 0)
            {
                isRightWalled = true;
            }
            if ((mask & enemyLayer) != 0)
            {
                isDead = true;
                Debug.Log("is Dead");
            }
        }
        if (isDead)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            moveSpeed = 0f;
        } 
        float horizontalInput = Input.GetAxis("Horizontal");
        spriteRenderer.flipX = (horizontalInput < 0);
        if ((!isLeftWalled || horizontalInput > 0) && (!isRightWalled || horizontalInput <= 0))
        {
            Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        isRightWalled = false;
        isLeftWalled = false;
        isGrounded = false;
        isDead = false;
    }
}
