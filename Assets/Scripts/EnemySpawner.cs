using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRB;

    public GameObject enemyPrefab;

    float spawnrate = 100f;
    float timeSinceSpawn = 100f;

    float minSpawnRadius = 7f;
    float maxSpawnRadius = 10f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // check if can spawn
        if (timeSinceSpawn >= spawnrate)
        {
            // spawn a random enemy at random position determined by random radius + random angle
            float spawnRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float spawnAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            // convert to coordinates
            Vector2 offset;
            offset.x = Mathf.Cos(spawnAngle) * spawnRadius;
            offset.y = Mathf.Sin(spawnAngle) * spawnRadius;

            // spawn the enemy and reset counter
            Instantiate(enemyPrefab, playerRB.position + offset, Quaternion.identity);
            timeSinceSpawn = 0f;
        }
        else
        {
            timeSinceSpawn += 1f;
        }
    }
}
