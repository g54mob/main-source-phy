using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace IE.RichFX
{
	[Serializable]
	public sealed class GradientParameter : VolumeParameter<Gradient>
	{
		protected override void OnEnable()
		{
			if (value == null)
			{
				value = GradientUtility.DefaultGradient;
			}
		}
	}
}
