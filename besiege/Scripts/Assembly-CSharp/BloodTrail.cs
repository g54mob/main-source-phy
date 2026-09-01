using UnityEngine;

[AddComponentMenu("VFX/BloodTrail")]
public class BloodTrail : MonoBehaviour
{
	public ParticleSystem pSystem;

	private float timeToDestroy;

	public float offsetBeforSpawned = 3f;

	private ParticleSystem.EmissionModule emisModule;

	private void Start()
	{
		timeToDestroy = pSystem.duration + pSystem.startLifetime;
		emisModule = pSystem.emission;
	}

	private void Update()
	{
		if (emisModule.enabled && pSystem.particleCount > 20)
		{
			emisModule.enabled = false;
			base.enabled = false;
			return;
		}
		if (timeToDestroy <= 0f)
		{
			base.enabled = false;
			Object.Destroy(base.gameObject);
			return;
		}
		timeToDestroy -= Time.deltaTime;
		Vector3 position = base.transform.position;
		if (position.y > WaterController.waterTransformHeight || position.y + offsetBeforSpawned > WaterController.CheckHeightMap(position.x, position.z))
		{
			emisModule.enabled = false;
		}
		else
		{
			emisModule.enabled = true;
		}
	}
}
