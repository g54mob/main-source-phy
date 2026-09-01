using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Property Controllers/LightController")]
	public class LightController : MonoBehaviour
	{
		[Header("Binding")]
		[MMInformation("Use this component to control the properties of one or more lights at runtime. Plays well with a FloatController. This component will try to auto set the TargetLight if there's a Light component on this object.", MMInformationAttribute.InformationType.Info, false)]
		public Light TargetLight;

		public List<Light> TargetLights;

		[Header("Light Settings")]
		public float Intensity;

		public float Multiplier;

		public float Range;

		[Header("Color")]
		public Color LightColor;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void ApplyLightSettings()
		{
		}
	}
}
