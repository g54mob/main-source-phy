using UnityEngine;

namespace LevelCreator
{
	public class Rotate : MonoBehaviour, ITriggerable
	{
		public bool rotate = true;

		public Vector3 rotation;

		public void Trigger()
		{
			rotate = !rotate;
		}

		private void Update()
		{
			if (rotate)
			{
				base.transform.Rotate(rotation * Time.deltaTime);
			}
		}
	}
}
