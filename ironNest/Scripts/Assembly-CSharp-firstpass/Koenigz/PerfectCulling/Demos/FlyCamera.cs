using UnityEngine;

namespace Koenigz.PerfectCulling.Demos
{
	public class FlyCamera : MonoBehaviour
	{
		[Range(30f, 150f)]
		[SerializeField]
		private float MouseSensitivity;

		private float m_rotationX;

		private float m_rotationY;

		private void LateUpdate()
		{
		}
	}
}
