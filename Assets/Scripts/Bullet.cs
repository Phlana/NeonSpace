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
        // gets color of body hit through material
        Color matColor = collision.GetComponent<Renderer>().material.color;
        Hit(matColor);
    }

    private void Hit(Color color)
    {
        Destroy(gameObject);  // destroys bullet when hit
        // spawning bullet hit effect
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // changing the color of hit effect to match whatever hit
        ParticleSystem ps = effect.GetComponentInChildren<ParticleSystem>();
        var psMain = ps.main;
        psMain.startColor = color;
        // destroys effect after 1 second
        Destroy(effect, 1f);
    }
}
