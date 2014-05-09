using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	private WeaponScript[] weapons;

	void Awake()
	{
		// retrieve all the weapons in child objects once
		weapons = GetComponentsInChildren<WeaponScript>();
	}

	void Update()
	{
		foreach (WeaponScript weapon in weapons)
		{
			// auto-fire
			if (weapon != null && weapon.CanAttack)
			{
				weapon.Attack (true);
			}
		}
	}
}