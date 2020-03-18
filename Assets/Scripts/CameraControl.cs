using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRB;
    Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        offset = transform.position - playerRB.transform.position;
    }
    private void Update()
    {
        transform.position = playerRB.transform.position + offset;
    }
}
