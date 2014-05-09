using UnityEngine;

public class WeaponScript : MonoBehaviour
{
	// projectile prefab for shooting
	public Transform shotPrefab;

	// cooldown in seconds between two shots
	public float shootingRate = 0.25f;

	private float shootCooldown;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			// create a new shot
			var shotTransform = Instantiate(shotPrefab) as Transform;

			// assign position
			shotTransform.position = transform.position;

			// the is-enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			// make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				move.direction = this.transform.right;
			}
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}