using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Camera/MMAspectRatioSafeZones")]
	public class MMAspectRatioSafeZones : MonoBehaviour
	{
		[Header("Center")]
		public bool DrawCenterCrosshair;

		public float CenterCrosshairSize;

		public Color CenterCrosshairColor;

		[Header("Ratios")]
		public bool DrawRatios;

		public float CameraSize;

		public float UnsafeZonesOpacity;

		public List<Ratio> Ratios;

		[MMInspectorButton("AutoSetup")]
		public bool AutoSetupButton;

		public virtual void AutoSetup()
		{
		}
	}
}
