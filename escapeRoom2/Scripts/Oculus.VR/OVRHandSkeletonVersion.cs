using System;
using UnityEngine;

public enum OVRHandSkeletonVersion
{
	[InspectorName(null)]
	Uninitialized = -1,
	[InspectorName("OVR Hand Skeleton")]
	OVR = 0,
	[InspectorName("OpenXR Hand Skeleton (Experimental)")]
	OpenXR = 1,
	[Obsolete("Use OVRinstead.")]
	[InspectorName("")]
	V1 = 0,
	[Obsolete("Use OpenXRinstead.")]
	[InspectorName("")]
	V2 = 1
}
