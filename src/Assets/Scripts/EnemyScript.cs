using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;

	void Awake()
	{
		// retrieve all the weapons in child objects once
		weapons = GetComponentsInChildren<WeaponScript>();

		// retrieve scripts to disable when not spawned
		moveScript = GetComponent<MoveScript>();
	}

	void Start()
	{
		hasSpawn = false;

		collider2D.enabled = false;
		moveScript.enabled = false;

		foreach (WeaponScript weapon in weapons)
			weapon.enabled = false;
	}

	void Update()
	{
		// check if spawned
		if (hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			// auto-fire
			foreach (WeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack(true);

					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}

			// out of the camera? destroy the game object
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}

	private void Spawn()
	{
		hasSpawn = true;

		// enable everything
		collider2D.enabled = true;
		moveScript.enabled = true;

		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}