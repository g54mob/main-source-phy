using UnityEngine;

namespace LevelCreator
{
	public class EarthquakeHazard : LandscapeHazard
	{
		[SerializeField]
		private string m_impactBrushKey = string.Empty;

		private Brush m_impactBrush;

		[SerializeField]
		private float m_destroyDelay = 1f;

		private void Start()
		{
			m_impactBrush = brushTable.GetBrush(m_impactBrushKey);
			ExecuteHazard();
			Object.Destroy(base.gameObject, m_destroyDelay);
		}

		private void ExecuteHazard()
		{
			DMEditor.Instance.VolumeRootObject.SubtractVolume(base.transform.position, m_impactBrush, Volume.fullLerpIntensity);
			DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, m_impactBrush));
			Shake();
			if (TakeSnapshot)
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
		}
	}
}
