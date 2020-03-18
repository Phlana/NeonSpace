using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    public float force = 1000f;
    float speed = 10f;
    public GameObject hitEffect;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // bullet's initial speed
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit();
    }

    public void Hit()
    {
        Destroy(gameObject);
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
}
