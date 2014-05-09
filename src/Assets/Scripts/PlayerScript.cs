using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Vector2 speed = new Vector2(50, 50);
	private Vector2 movement;

	void Update()
	{
		// get input
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// change movement vector
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);

		// shooting
		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");

		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				// false passed because player is not an enemy
				weapon.Attack(false);
			}
		}
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}
}