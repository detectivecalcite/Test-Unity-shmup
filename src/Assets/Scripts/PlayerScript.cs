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

		// make sure we are not outside the camer bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).x;

		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
		).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
		).y;

		transform.position = new Vector3(
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;

		// collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
		
			damagePlayer = true;
		}

		// damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
}