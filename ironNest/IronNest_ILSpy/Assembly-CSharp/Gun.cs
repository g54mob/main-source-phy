using System;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class Gun
{
	public string gunName;

	public RectTransform firePoint2D;

	public Transform chamberSlot;

	public RectTransform impactMarker2D;

	public GameObject noShellImpactMarkerPrefab;

	public RectTransform shellParent2D;

	public Animator gunAnimator;

	public Turret3DMimic mimic3D;

	public float barrelElevation;

	public float minElevation;

	public float maxElevation;

	public KeyCode fireKey;

	public float fireDelay;

	public GameObject activeMarkerVisual;

	public GameObject noShellMarkerInstance;

	public string lastActiveMarkerName;

	public int currentBarrelIndex;

	public float GetElevation()
	{
		return barrelElevation;
	}

	public float GetMinElevation()
	{
		return minElevation;
	}

	public float GetMaxElevation()
	{
		return maxElevation;
	}

	public Gun()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AAE4]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		gunName = "Gun";
		barrelElevation = 500f;
		maxElevation = 1000f;
		fireKey = KeyCode.Space;
		lastActiveMarkerName = "";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
