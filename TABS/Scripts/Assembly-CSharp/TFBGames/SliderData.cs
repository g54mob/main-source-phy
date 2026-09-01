using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public struct SliderData
	{
		public float min;

		public float max;

		public float current;

		[Header("Rounding")]
		public bool useRounding;

		public int roundToNearest;

		public float GetRoundedValue(float value)
		{
			return Mathf.Round(value / (float)roundToNearest) * (float)roundToNearest;
		}
	}
}
