using UnityEngine;

public class SetEmissionByScale : MonoBehaviour
{
	public ParticleSystem[] particles;

	public Vector3 startScale = Vector3.zero;

	public short maxParticles = short.MaxValue;

	protected Vector3 currentScale;

	protected float[] startEmission;

	protected short[] startBursts;

	protected ParticleSystem.Burst[] bursts;

	private bool hasBurst;

	protected void Awake()
	{
		if (startScale == Vector3.zero)
		{
			startScale = base.transform.lossyScale;
		}
		currentScale = base.transform.lossyScale;
		startEmission = new float[particles.Length];
		startBursts = new short[particles.Length];
		bursts = new ParticleSystem.Burst[particles.Length];
		for (int i = 0; i < startEmission.Length; i++)
		{
			ParticleSystem particleSystem = particles[i];
			startEmission[i] = particleSystem.emission.rate.constant;
			if (particleSystem.emission.burstCount > 0)
			{
				hasBurst = true;
				ParticleSystem.Burst[] array = new ParticleSystem.Burst[particleSystem.emission.burstCount];
				particleSystem.emission.GetBursts(array);
				bursts[i] = array[0];
				startBursts[i] = bursts[i].maxCount;
			}
			particleSystem.Stop();
			particleSystem.randomSeed = (uint)Random.Range(0, 9999999);
			particleSystem.Play();
		}
		SetEmission();
	}

	protected void Update()
	{
		if (currentScale != base.transform.lossyScale)
		{
			SetEmission();
		}
	}

	protected void SetEmission()
	{
		currentScale = base.transform.lossyScale;
		float num = currentScale.sqrMagnitude / startScale.sqrMagnitude;
		short num2 = 0;
		ParticleSystem.Burst[] array = new ParticleSystem.Burst[1];
		for (int i = 0; i < particles.Length; i++)
		{
			ParticleSystem.EmissionModule emission = particles[i].emission;
			emission.rate = Mathf.Min(maxParticles, num * startEmission[i]);
			if (hasBurst && startBursts[i] > 0)
			{
				num2 = (short)Mathf.Min(maxParticles, (float)startBursts[i] * num);
				ParticleSystem.Burst[] array2 = bursts;
				int num3 = i;
				short num4 = num2;
				bursts[i].maxCount = num4;
				array2[num3].minCount = num4;
				array[0] = bursts[i];
				emission.SetBursts(array);
			}
		}
	}
}
