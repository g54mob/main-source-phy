using UnityEngine;

namespace MoreMountains.Tools
{
	public static class MMMaths
	{
		private static float SpringVelocity(float currentValue, float targetValue, float velocity, float damping, float frequency, float deltaTime)
		{
			return 0f;
		}

		public static void Spring(ref float currentValue, float targetValue, ref float velocity, float damping, float frequency, float deltaTime)
		{
		}

		public static void Spring(ref Vector2 currentValue, Vector2 targetValue, ref Vector2 velocity, float damping, float frequency, float deltaTime)
		{
		}

		public static void Spring(ref Vector3 currentValue, Vector3 targetValue, ref Vector3 velocity, float damping, float frequency, float deltaTime)
		{
		}

		public static void Spring(ref Vector4 currentValue, Vector4 targetValue, ref Vector4 velocity, float damping, float frequency, float deltaTime)
		{
		}

		private static float LerpRate(float rate, float deltaTime)
		{
			return 0f;
		}

		public static float Lerp(float value, float target, float rate, float deltaTime)
		{
			return 0f;
		}

		public static Vector2 Lerp(Vector2 value, Vector2 target, float rate, float deltaTime)
		{
			return default(Vector2);
		}

		public static Vector3 Lerp(Vector3 value, Vector3 target, float rate, float deltaTime)
		{
			return default(Vector3);
		}

		public static Vector4 Lerp(Vector4 value, Vector4 target, float rate, float deltaTime)
		{
			return default(Vector4);
		}

		public static Quaternion Lerp(Quaternion value, Quaternion target, float rate, float deltaTime)
		{
			return default(Quaternion);
		}

		public static Color Lerp(Color value, Color target, float rate, float deltaTime)
		{
			return default(Color);
		}

		public static Color32 Lerp(Color32 value, Color32 target, float rate, float deltaTime)
		{
			return default(Color32);
		}

		public static float Clamp(float value, float min, float max, bool clampMin, bool clampMax)
		{
			return 0f;
		}

		public static float RoundToNearestHalf(float a)
		{
			return 0f;
		}

		public static Quaternion LookAt2D(Vector2 direction)
		{
			return default(Quaternion);
		}

		public static Vector2 Vector3ToVector2(Vector3 target)
		{
			return default(Vector2);
		}

		public static Vector3 Vector2ToVector3(Vector2 target)
		{
			return default(Vector3);
		}

		public static Vector3 Vector2ToVector3(Vector2 target, float newZValue)
		{
			return default(Vector3);
		}

		public static Vector3 RoundVector3(Vector3 vector)
		{
			return default(Vector3);
		}

		public static Vector2 RandomVector2(Vector2 minimum, Vector2 maximum)
		{
			return default(Vector2);
		}

		public static Vector3 RandomVector3(Vector3 minimum, Vector3 maximum)
		{
			return default(Vector3);
		}

		public static Vector2 RandomPointOnCircle(float circleRadius)
		{
			return default(Vector2);
		}

		public static Vector3 RandomPointOnSphere(float sphereRadius)
		{
			return default(Vector3);
		}

		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
		{
			return default(Vector3);
		}

		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
		{
			return default(Vector3);
		}

		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion quaternion)
		{
			return default(Vector3);
		}

		public static Vector2 RotateVector2(Vector2 vector, float angle)
		{
			return default(Vector2);
		}

		public static float AngleBetween(Vector2 vectorA, Vector2 vectorB)
		{
			return 0f;
		}

		public static float AngleDirection(Vector3 vectorA, Vector3 vectorB, Vector3 up)
		{
			return 0f;
		}

		public static float DistanceBetweenPointAndLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
		{
			return 0f;
		}

		public static Vector3 ProjectPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
		{
			return default(Vector3);
		}

		public static int Sum(params int[] thingsToAdd)
		{
			return 0;
		}

		public static int RollADice(int numberOfSides)
		{
			return 0;
		}

		public static bool Chance(int percent)
		{
			return false;
		}

		public static float Approach(float from, float to, float amount)
		{
			return 0f;
		}

		public static float Remap(float x, float A, float B, float C, float D)
		{
			return 0f;
		}

		public static float ClampAngle(float angle, float minimumAngle, float maximumAngle)
		{
			return 0f;
		}

		public static float RoundToDecimal(float value, int numberOfDecimals)
		{
			return 0f;
		}

		public static float RoundToClosest(float value, float[] possibleValues, bool pickSmallestDistance = false)
		{
			return 0f;
		}

		public static Vector3 DirectionFromAngle(float angle, float additionalAngle)
		{
			return default(Vector3);
		}

		public static Vector3 DirectionFromAngle2D(float angle, float additionalAngle)
		{
			return default(Vector3);
		}
	}
}
