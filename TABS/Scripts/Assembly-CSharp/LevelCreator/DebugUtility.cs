using UnityEngine;

namespace LevelCreator
{
	public class DebugUtility
	{
		public static void DrawPositionMarker(Vector3 position, Color color)
		{
			Debug.DrawLine(position + Vector3.left * 0.4f, position + Vector3.right * 0.4f, color, 15f);
			Debug.DrawLine(position + Vector3.down * 0.4f, position + Vector3.up * 0.4f, color, 15f);
			Debug.DrawLine(position + Vector3.back * 0.4f, position + Vector3.forward * 0.4f, color, 15f);
		}

		public static void DrawRotation(Vector3 position, Quaternion rotation)
		{
			Debug.DrawLine(position, position + rotation * Vector3.up * 5f, Color.red);
			Debug.DrawLine(position, position + rotation * Vector3.right * 5f, Color.green);
			Debug.DrawLine(position, position + rotation * Vector3.forward * 5f, Color.blue);
		}
	}
}
