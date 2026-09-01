using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class FogSettings
	{
		public bool FogEnabled;

		public Color FogColor;

		public float FogDensity;

		public FogMode FogMode;
	}
}
