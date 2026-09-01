using UnityEngine;

namespace LevelCreator
{
	public class ParticleSystemPrctScale : MonoBehaviour
	{
		public float ScalePrct;

		private ParticleSystem ps;

		[Header("Particle Settings, X = (Scale = 0), Y = (Scale = 1)")]
		public Vector2 emissionRate;

		public Vector2 lifeTime;

		public Vector2 size;

		public Vector2 speed;

		private void Start()
		{
			ps = GetComponent<ParticleSystem>();
			SetPrct(ScalePrct);
		}

		public void SetPrct(float prct)
		{
			ScalePrct = Mathf.Clamp01(prct);
			UpdateParticleSystem();
		}

		private void UpdateParticleSystem()
		{
			ParticleSystem.EmissionModule emission = ps.emission;
			emission.rateOverTime = Mathf.Lerp(emissionRate.x, emissionRate.y, ScalePrct);
			ParticleSystem.MainModule main = ps.main;
			main.startLifetimeMultiplier = Mathf.Lerp(lifeTime.x, lifeTime.y, ScalePrct);
			main.startSizeMultiplier = Mathf.Lerp(size.x, size.y, ScalePrct);
			main.startSpeedMultiplier = Mathf.Lerp(speed.x, speed.y, ScalePrct);
		}
	}
}
