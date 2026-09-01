using UnityEngine;

namespace LevelCreator
{
	public class LightningHazard : LandscapeHazard
	{
		private Brush m_impactBrush;

		[Space]
		[SerializeField]
		private float m_destroyDelay = 1f;

		[SerializeField]
		private ParticleSystem m_chargeParticles;

		private Brush GenerateChargeUpBrush(int size)
		{
			return VolumeBrushes.CreateConeBrush(size, size, 0.04f, 0f);
		}

		private Brush GenerateImpactBrush()
		{
			return VolumeBrushes.CreateConeBrush(10, 10, 0.04f, 0f);
		}

		private void Start()
		{
			m_chargeParticles.transform.localScale = Vector3.one * Mathf.Clamp01(ChargePrct);
			if (ChargePrct > 0f)
			{
				int size = (int)Mathf.Lerp(1f, 25f, ChargePrct);
				m_impactBrush = GenerateChargeUpBrush(size);
			}
			else
			{
				m_impactBrush = GenerateImpactBrush();
			}
			ExecuteHazard();
			Object.Destroy(base.gameObject, m_destroyDelay);
		}

		private void ExecuteHazard()
		{
			DMEditor.Instance.VolumeRootObject.SubtractVolume(base.transform.position, m_impactBrush, Volume.fullLerpIntensity);
			Shake();
			DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, m_impactBrush));
			if (TakeSnapshot)
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
		}
	}
}
