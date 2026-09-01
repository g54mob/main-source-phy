using UnityEngine;

namespace LevelCreator
{
	public abstract class LandscapeHazard : MonoBehaviour
	{
		[SerializeField]
		protected VolumeBrushTable brushTable;

		[Space]
		[SerializeField]
		protected GameObject onDestroyInstantiate;

		[HideInInspector]
		public bool TakeSnapshot;

		[HideInInspector]
		public float ChargePrct;

		protected void Shake()
		{
			if (ScreenShake.Instance != null)
			{
				ScreenShake.Instance.AddForce(Vector3.up * 8f, base.transform.position);
			}
		}

		protected void OnDestroy()
		{
			if ((bool)onDestroyInstantiate)
			{
				Object.Instantiate(onDestroyInstantiate, base.transform.position, Quaternion.identity);
			}
		}
	}
}
