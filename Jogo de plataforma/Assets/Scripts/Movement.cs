using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float xVelocity = 10f, jumpForce = 8f;
    [SerializeField] bool isJump, inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask onGround;


    private void Start()
    {
        dead = false;

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (dead) return;

        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, onGround);

        if (Input.GetButtonDown("Jump") && inFloor)
            isJump = true;
        else if (Input.GetButtonUp("Jump") && rb.velocity.y >0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5F);
    }

    void Moviment()
    {
        if (dead) return;

        float xMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xMove * xVelocity, rb.velocity.y);
    }
    
    void Jump()
    {
        if (dead) return;

        if (isJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJump = false; 
        }
    }

    private void FixedUpdate()
    {
        Moviment();
        Jump();
    }

    public int coin;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coin++;
        }
    }

    [SerializeField] bool dead = false;
    CircleCollider2D playerCollider;

    public void Death()
    {
        StartCoroutine(DeathCorotine());
    }
    IEnumerator DeathCorotine()
    {
        if (!dead)
        {
            dead = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;           
            yield return new WaitForSeconds(0.5f);
            rb.gravityScale = 1f;
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            playerCollider.isTrigger = true;
            Invoke("RestartGame", 2.5f);

        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
  
}
