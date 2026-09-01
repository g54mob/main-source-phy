using UnityEngine;

namespace LevelCreator
{
	public class VulcanoProjectile : LandscapeHazard
	{
		public string impactBrushKey;

		private Brush impactBrush;

		public float destroyDelay = 7f;

		private void Start()
		{
			impactBrush = brushTable.GetBrush(impactBrushKey);
			Object.Destroy(base.transform.parent.gameObject, destroyDelay);
		}

		private void OnCollisionEnter(Collision other)
		{
			ExecuteHazard();
		}

		private void ExecuteHazard()
		{
			DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, impactBrush));
		}
	}
}
