using UnityEngine;

public class PlayerFire : MonoBehaviour
{
	public GameObject bulletPrefab;

	// how fast the player shoots
	float shootSpeed = 20f;
	float timeFromLastShot = 20f;

	void FixedUpdate()
	{
		// check if can fire
		if (timeFromLastShot >= shootSpeed)
		{
			if (Input.GetButton("Fire1"))
			{
				Shoot();
				timeFromLastShot = 0f;
			}
		}
		else if (timeFromLastShot < shootSpeed)
		{
			timeFromLastShot += 1f;
		}
	}

	void Shoot()
	{
		GameObject bulletObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
		// destroy bullets after 1 second if nothing is hit
		Destroy(bulletObject, 1f);
	}
}
