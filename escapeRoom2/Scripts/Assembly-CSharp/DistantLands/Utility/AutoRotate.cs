using UnityEngine;

namespace DistantLands.Utility
{
	public class AutoRotate : MonoBehaviour
	{
		public Vector3 rotateAngle;

		private void Update()
		{
			base.transform.eulerAngles += rotateAngle * Time.deltaTime;
		}
	}
}
