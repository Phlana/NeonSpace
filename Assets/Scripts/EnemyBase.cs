using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 100;
    public float speed = 3f;
    public GameObject deathEffect;
    private Rigidbody2D rb;

    // player to track
    private GameObject player;
    private Rigidbody2D playerRB;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // get reference to the player
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }

    public virtual void Move(float delTime)
    {
        // always rotates towards player
        Vector2 lookDir = playerRB.position - rb.position;
        float angleToPlayer = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angleToPlayer;

        // add velocity towards player and clamp
        rb.velocity += lookDir.normalized * 0.1f;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleCollider2D collider = collision.GetComponent<CapsuleCollider2D>();
        if (collider.CompareTag("PlayerBullet"))
        {
            // get bullet
            Bullet bullet = collider.GetComponent<Bullet>();
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            // apply damage
            TakeDamage(bullet.damage);

            // apply knockback
            TakeKnockback(bullet.force, bulletRB.velocity.normalized);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeKnockback(float amount, Vector2 direction)
    {
        rb.AddForce(amount * direction);
    }

    public void Die()
    {
        Destroy(gameObject);  // destroys this enemy
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
    }
}
