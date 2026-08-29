using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoomableSettings : ScriptableObject
{
	[Serializable]
	public class AspectRatioRenderEntry
	{
		public string label;

		public float aspectRatio;
	}

	[Header("Gizmos - Draw mode")]
	public bool drawGizmosOnlyIfSelected = true;

	[Header("Gizmos - Camera frustum")]
	public bool showCameraGizmos = true;

	public float cameraMaxRange = 0.3f;

	public float cameraMinRange;

	public float cameraAspect = 1.7777778f;

	public Color cameraFrustumColor = Color.magenta;

	[Header("Gizmos - Eye line of sight")]
	public bool showEyeLineOfSight = true;

	public Color eyeLineOfSightColor = Color.yellow;

	[Header("Gizmos - Pivot")]
	public bool showPivot = true;

	public float pivotRadius = 0.01f;

	public Color pivotColor = Color.magenta;

	[Header("Gizmos - Eye")]
	public bool showEye = true;

	public float eyeRadius = 0.01f;

	public Color eyeColor = Color.yellow;

	[Header("Inspector")]
	public bool showAspectRatioRenderingsInPlayMode = true;

	public Color asprectRatioRenderingsTitleColor = new Color(0f, 0.65f, 1f);

	public bool useIsolatedView;

	[NonReorderable]
	public List<AspectRatioRenderEntry> aspectRatioRenderEntries = new List<AspectRatioRenderEntry>
	{
		new AspectRatioRenderEntry
		{
			label = "Full HD (16:9)",
			aspectRatio = 1.7777778f
		},
		new AspectRatioRenderEntry
		{
			label = "Ultra-wide (21:9)",
			aspectRatio = 2.3333333f
		}
	};
}
