using UnityEngine;

namespace Poly.Extension
{
	public static class QuaternionExtension
	{
		public static void ToAngleAxisSafe(this Quaternion q, out float angle, out Vector3 axis)
		{
			q.ToAngleAxis(out angle, out axis);
			angle = AngleToSigned180(angle);
			if (angle < 0f)
			{
				angle *= -1f;
				axis *= -1f;
			}
		}

		public static Vector3 GetSafeSignedEuler(this Quaternion q)
		{
			Vector3 eulerAngles = q.eulerAngles;
			eulerAngles.x = AngleToSigned180(eulerAngles.x);
			eulerAngles.y = AngleToSigned180(eulerAngles.y);
			eulerAngles.z = AngleToSigned180(eulerAngles.z);
			return eulerAngles;
		}

		public static float AngleToSigned180(float angle)
		{
			if (angle > 180f)
			{
				angle -= 360f;
			}
			else if (angle <= -180f)
			{
				angle += 360f;
			}
			return angle;
		}
	}
}
