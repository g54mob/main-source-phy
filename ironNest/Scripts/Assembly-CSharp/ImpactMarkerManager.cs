using System.Collections.Generic;
using UnityEngine;

public class ImpactMarkerManager : MonoBehaviour
{
	private class MarkerData
	{
		public GunController gun;

		public RectTransform container;

		public GameObject activeMarkerInstance;

		public GameObject noShellMarkerInstance;

		public string lastMarkerName;
	}

	[Header("Core References")]
	public TurretController turretController;

	[Header("Marker Prefabs")]
	public GameObject noShellImpactMarkerPrefab;

	public GameObject masterImpactMarkerPrefab;

	private List<MarkerData> markerDataList;

	private GameObject masterImpactMarkerInstance;

	private void Start()
	{
	}

	private void LateUpdate()
	{
	}

	private void SetupAllMarkers()
	{
	}

	private void SetupMasterMarker()
	{
	}

	private void UpdateAllGunMarkers()
	{
	}

	private void UpdateMasterMarker()
	{
	}

	private float CalculateProjectedRangeFromElevation(float elevation)
	{
		return 0f;
	}
}
