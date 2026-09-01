using UnityEngine;

namespace TFBGames
{
	public static class Vector3Extensions
	{
		public static Vector3 InvertAxis(this Vector3 vector, Vector3Axis axis)
		{
			switch (axis)
			{
			case Vector3Axis.X:
				vector = new Vector3(vector.x * -1f, vector.y, vector.z);
				break;
			case Vector3Axis.Y:
				vector = new Vector3(vector.x, vector.y * -1f, vector.z);
				break;
			case Vector3Axis.Z:
				vector = new Vector3(vector.x, vector.y, vector.z * -1f);
				break;
			default:
				return vector;
			}
			return vector;
		}
	}
}
