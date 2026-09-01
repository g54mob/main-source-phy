using UnityEngine;

[AddComponentMenu("Water/Objects/Water Godrays")]
public class WaterGRayParticle : MonoBehaviour
{
	public ParticleSystem pSystem;

	public Transform camTransform;

	public Transform lightTransform;

	public int particleAmount = 50;

	public float radius = 20f;

	public float minRadius = 5f;

	public float heightOffset = -1f;

	private ParticleSystem.EmitParams emitParams;

	private Vector3 randomOffset;

	private Vector3 particlePos;

	private float timeToNextParticle;

	private float time;

	private void Start()
	{
		if (camTransform == null)
		{
			camTransform = Camera.main.transform;
		}
	}

	private void Update()
	{
		timeToNextParticle = 1f / (float)particleAmount;
		time += Time.deltaTime;
		int num = (int)(time / timeToNextParticle);
		time -= timeToNextParticle * (float)num;
		EmitParticle(num);
	}

	private void EmitParticle(int count)
	{
		for (int i = 0; i < count; i++)
		{
			randomOffset = Random.onUnitSphere;
			randomOffset.y = 0f;
			randomOffset = randomOffset.normalized * Random.Range(minRadius, radius);
			particlePos = camTransform.position + camTransform.forward * minRadius + randomOffset;
			particlePos.y = WaterController.CheckHeightMap(emitParams.position.x, emitParams.position.z) - heightOffset + Random.Range(Random.Range(-3f, -1f), 1f);
			emitParams.position = particlePos;
			pSystem.Emit(emitParams, 1);
		}
	}
}
