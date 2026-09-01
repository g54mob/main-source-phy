using Landfall.TABS;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class CameraMovementCollision : MonoBehaviour
{
	[SerializeField]
	private CameraAbilityPossess cameraAbilityPossess;

	private RotationShake softRotationShake;

	public float multiplier = 1f;

	public float radius = 0.3f;

	public LayerMask mask;

	private Vector3 lastPos;

	private SphereCollider col;

	private CameraMovement cameraMove;

	public string soundRef;

	private SoundPlayer m_soundPlayer;

	private SandboxPlacementCamera sandPlace;

	private UnitEditorFreeCamera UnitEditorFreeCamera;

	private Collider[] overLapSphereHits = new Collider[36];

	private NativeArray<RaycastCommand> raycastCommands;

	private NativeArray<RaycastHit> raycastHits;

	private JobHandle jobHandle;

	private bool doRaycasts;

	private void Awake()
	{
		raycastCommands = new NativeArray<RaycastCommand>(1, Allocator.Persistent);
		raycastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
	}

	private void Start()
	{
		softRotationShake = GetComponentInChildren<RotationShake>();
		cameraMove = GetComponent<CameraMovement>();
		sandPlace = GetComponent<SandboxPlacementCamera>();
		col = base.gameObject.AddComponent<SphereCollider>();
		col.radius = radius;
		lastPos = base.transform.position;
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		UnitEditorFreeCamera = GetComponent<UnitEditorFreeCamera>();
	}

	private void Update()
	{
		doRaycasts = ((bool)sandPlace && !sandPlace.IsReturning) || UnitEditorFreeCamera != null;
		if (cameraAbilityPossess != null)
		{
			doRaycasts = doRaycasts && !cameraAbilityPossess.IsPossessing;
		}
		if (doRaycasts)
		{
			raycastCommands[0] = new RaycastCommand(lastPos, (base.transform.position - lastPos).normalized, Vector3.Distance(base.transform.position, lastPos), mask);
			jobHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 1);
		}
	}

	private void LateUpdate()
	{
		Vector3 vector = base.transform.position;
		if (doRaycasts)
		{
			jobHandle.Complete();
			float num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
			col.enabled = true;
			int num2 = Physics.OverlapSphereNonAlloc(vector, radius, overLapSphereHits, mask);
			for (int i = 0; i < num2; i++)
			{
				Collider collider = overLapSphereHits[i];
				if (collider == col)
				{
					continue;
				}
				Vector3 velocity = cameraMove.Velocity;
				Physics.ComputePenetration(col, vector, base.transform.rotation, collider, collider.transform.position, collider.transform.rotation, out var direction, out var distance);
				col.transform.position += direction * distance;
				if (float.IsNaN(direction.x))
				{
					direction.x = 0f;
				}
				if (float.IsNaN(direction.y))
				{
					direction.y = 0f;
				}
				if (float.IsNaN(direction.z))
				{
					direction.z = 0f;
				}
				Vector3 vector2 = Vector3.ProjectOnPlane(cameraMove.Velocity, direction);
				if (Vector3.Angle(cameraMove.Velocity, direction) > 90f)
				{
					_ = velocity - vector2;
					cameraMove.Velocity = vector2;
					ScreenShake.Instance.AddForce((velocity - cameraMove.Velocity) * 2f * distance, vector + base.transform.forward);
					cameraMove.Velocity += direction * distance * multiplier * Time.deltaTime;
					softRotationShake.AddForce(num * (0f - base.transform.InverseTransformDirection(cameraMove.Velocity).x) * Vector3.up, vector + base.transform.right);
					softRotationShake.AddForce(num * (0f - base.transform.InverseTransformDirection(cameraMove.Velocity).z) * Vector3.up, vector + base.transform.forward);
					float num3 = (velocity - cameraMove.Velocity).magnitude * 0.3f;
					if (num3 > 1f)
					{
						m_soundPlayer.PlaySoundEffect(soundRef, num3 * 0.01f, vector, SoundEffectVariations.MaterialType.Default, base.transform);
					}
				}
			}
			col.enabled = false;
			RaycastHit raycastHit = raycastHits[0];
			if (raycastHit.collider != null)
			{
				vector = raycastHit.point + raycastHit.normal * 0.1f;
			}
		}
		if (float.IsNaN(vector.x))
		{
			vector = Vector3.zero;
		}
		vector = (lastPos = Vector3.ClampMagnitude(vector, 1000f));
		base.transform.position = vector;
	}

	private void OnDestroy()
	{
		jobHandle.Complete();
		raycastCommands.Dispose();
		raycastHits.Dispose();
	}
}
