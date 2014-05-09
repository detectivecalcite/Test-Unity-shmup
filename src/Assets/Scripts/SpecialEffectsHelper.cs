using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
	public static SpecialEffectsHelper Instance;

	public ParticleSystem smokeEffect;
	public ParticleSystem fireEffect;

	void Awake()
	{
		// register the singleton
		if (Instance != null)
		{
			Debug.LogError ("Multiple instances of SpecialEffectsHelper");
		}

		Instance = this;
	}

	// create an explosion at the given location
	public void Explosion(Vector3 position)
	{
		instantiate(smokeEffect, position);
		instantiate(fireEffect, position);
	}

	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(
			prefab,
			position,
			Quaternion.identity
		) as ParticleSystem;

		Destroy(
			newParticleSystem.gameObject,
			newParticleSystem.startLifetime
		);

		return newParticleSystem;
	}
}