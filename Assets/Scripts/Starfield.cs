using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRB;

    private Rect screenRect;
    private ParticleSystem ps;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        // get the screen rect with margins
        int margin = 500;
        screenRect = new Rect(-margin, -margin, Screen.width + margin, Screen.height + margin);

        // change the particle field to match screenRect TODO

        // immediately generate some number of stars
        ps.Emit(25);
    }

    void Update()
    {
        /*
         * we want the particle system to track the player like the camera does
         * but we want the particles within the particle system to appear stationary
         */

        // so first get the displacement
        Vector3 displacement = playerRB.transform.position - transform.position;
        // then change the particle system's position
        transform.position = playerRB.transform.position;
        // then change each particles' positions by the displacement TODO

        // if star goes out of screen, place new one on other side of screen rect
        // what we can do:
        // map coordinates to coordinates mod rect width/heights
    }
}
