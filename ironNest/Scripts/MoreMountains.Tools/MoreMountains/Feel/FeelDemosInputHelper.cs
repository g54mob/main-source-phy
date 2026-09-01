using UnityEngine;

namespace MoreMountains.Feel
{
	public static class FeelDemosInputHelper
	{
		private const string _horizontalAxis = "Horizontal";

		private const string _verticalAxis = "Vertical";

		public static bool CheckMainActionInputPressedThisFrame()
		{
			return false;
		}

		public static bool CheckMainActionInputPressed()
		{
			return false;
		}

		public static bool CheckMainActionInputUpThisFrame()
		{
			return false;
		}

		public static bool CheckEnterPressedThisFrame()
		{
			return false;
		}

		public static bool CheckMouseDown()
		{
			return false;
		}

		public static Vector2 MousePosition()
		{
			return default(Vector2);
		}

		public static Vector2 GetDirectionAxis(ref Vector2 direction)
		{
			return default(Vector2);
		}

		public static bool CheckAlphaInputPressedThisFrame(int alpha)
		{
			return false;
		}
	}
}
