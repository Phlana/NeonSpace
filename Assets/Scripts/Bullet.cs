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
        if (!collision.gameObject.CompareTag("Player"))
        {
            // gets material of body hit through material
            Material mat = collision.GetComponent<Renderer>().material;
            Hit(mat);
        }
    }

    private void Hit(Material mat)
    {
        Destroy(gameObject);  // destroys bullet when hit

        // spawning bullet hit effect
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

        // changing properties of hit effect to match whatever hit
        ParticleSystem ps = effect.GetComponentInChildren<ParticleSystem>();

        // particle material
        Renderer psRenderer = ps.GetComponent<Renderer>();
        psRenderer.material = mat;

        // particle color for trail inheritance
        var psMain = ps.main;
        psMain.startColor = 0.6f * mat.color;

        // destroys effect after 1 second
        Destroy(effect, 1f);
    }
}
