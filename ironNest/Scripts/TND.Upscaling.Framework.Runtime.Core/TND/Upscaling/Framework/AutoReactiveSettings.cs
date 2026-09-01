using System;
using UnityEngine;

namespace TND.Upscaling.Framework
{
	[Serializable]
	public struct AutoReactiveSettings
	{
		[Range(0f, 1f)]
		public float scale;

		[Range(0f, 1f)]
		public float cutoffThreshold;

		[Range(0f, 1f)]
		public float binaryValue;

		public AutoReactiveFlags flags;

		public static readonly AutoReactiveSettings Default;
	}
}
