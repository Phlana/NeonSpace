using UnityEngine;

public class Starfield : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRB;
    Vector3 offset;

    Vector2 size;
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    public int numStars = 25;
    public float parallaxScale = 0.8f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        offset = transform.position - playerRB.transform.position;

        // get sizes of camera / viewport
        Vector2 botleft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topright = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // set size of starfield with margins
        size = topright - botleft + new Vector2(10, 10);

        // change the particle field to match size
        var sh = ps.shape;
        sh.scale = size;

        // set the max amount of particles
        var mn = ps.main;
        mn.maxParticles = numStars;

        // immediately generate some number of stars
        ps.Emit(numStars);
    }

    void Update()
    {
        MoveAndCheckStars();
    }

    void MoveAndCheckStars()
    {
        /*
         * we want the particle system to track the player like the camera does
         * but we want the particles within the particle system to appear stationary
         */

        // so first get the displacement and reset z axis
        Vector3 displacement = playerRB.transform.position - transform.position;
        displacement.z = 0;
        // then change the particle system's position
        transform.position = playerRB.transform.position + offset;
        // then change each particles' positions by the displacement
        int amount = ps.GetParticles(particles);

        for (int i = 0; i < amount; i++)
        {
            particles[i].position -= displacement * parallaxScale;

            // boundaries
            float bottom = -size.y / 2f;
            float top = size.y / 2f;
            float left = -size.x / 2f;
            float right = size.x / 2f;

            // if particle crosses boundary, move particle to the other side with random other coordinate
            if (particles[i].position.x < left)
            {
                Vector2 newPos = new Vector2(right, Random.Range(bottom, top));
                particles[i].position = newPos;
            }
            if (particles[i].position.x > right)
            {
                Vector2 newPos = new Vector2(left, Random.Range(bottom, top));
                particles[i].position = newPos;
            }
            if (particles[i].position.y < bottom)
            {
                Vector2 newPos = new Vector2(Random.Range(left, right), top);
                particles[i].position = newPos;
            }
            if (particles[i].position.y > top)
            {
                Vector2 newPos = new Vector2(Random.Range(left, right), bottom);
                particles[i].position = newPos;
            }
        }

        ps.SetParticles(particles);
    }
}
