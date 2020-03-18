using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Death")) {
            // player is now dead
            Debug.Log("i died i am dead");
            Destroy(gameObject);
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
