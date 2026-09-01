using System;
using UnityEngine;

[Serializable]
public class Gun
{
	[Header("General")]
	public string gunName;

	[Header("References")]
	public RectTransform firePoint2D;

	public Transform chamberSlot;

	public RectTransform impactMarker2D;

	public GameObject noShellImpactMarkerPrefab;

	public RectTransform shellParent2D;

	public Animator gunAnimator;

	[Header("3D Mimic")]
	public Turret3DMimic mimic3D;

	[Header("Barrel & Firing")]
	public float barrelElevation;

	public float minElevation;

	public float maxElevation;

	public KeyCode fireKey;

	public float fireDelay;

	[HideInInspector]
	public GameObject activeMarkerVisual;

	[HideInInspector]
	public GameObject noShellMarkerInstance;

	[HideInInspector]
	public string lastActiveMarkerName;

	[HideInInspector]
	public int currentBarrelIndex;

	public float GetElevation()
	{
		return 0f;
	}

	public float GetMinElevation()
	{
		return 0f;
	}

	public float GetMaxElevation()
	{
		return 0f;
	}
}
