using UnityEngine;

public class Turret3DMimic : MonoBehaviour
{
	public enum Axis
	{
		X = 0,
		Y = 1,
		Z = 2
	}

	[Header("References")]
	public TurretController turretController;

	public Transform turretBase3D;

	public Transform[] barrelPivots;

	public GameObject[] muzzleFlashPrefabs;

	[Header("Axis & Inversion")]
	public Axis turretRotationAxis;

	public Axis barrelElevationAxis;

	public bool invertRotation;

	public bool invertElevation;

	[Header("Offsets")]
	public Vector3 turretRotationOffset;

	public Vector3 barrelElevationOffset;

	private void Update()
	{
	}

	public void SetElevationMapping(float minElevation, float maxElevation)
	{
	}

	public void SyncTurret(float currentAngle)
	{
	}

	public void OnFireBarrel(int barrelIndex)
	{
	}

	private Quaternion RotationWithAxis(float angle, Axis axis)
	{
		return default(Quaternion);
	}
}
