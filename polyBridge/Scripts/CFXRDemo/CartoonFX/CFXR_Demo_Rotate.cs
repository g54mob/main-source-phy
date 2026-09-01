using UnityEngine;

namespace CartoonFX
{
	public class CFXR_Demo_Rotate : MonoBehaviour
	{
		public Vector3 axis = new Vector3(0f, 1f, 0f);

		public Vector3 center;

		public float speed = 1f;

		private void Update()
		{
			base.transform.RotateAround(center, axis, speed * Time.deltaTime);
		}
	}
}
