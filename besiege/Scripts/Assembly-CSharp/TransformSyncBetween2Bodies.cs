using UnityEngine;

[AddComponentMenu("Physics/Client/TransformSyncBetween2Bodies")]
public class TransformSyncBetween2Bodies : MonoBehaviour
{
	public bool forceAlways;

	public Transform start;

	public Transform end;

	public Vector3 startPoint;

	public Vector3 endPoint;

	[HideInInspector]
	[SerializeField]
	private Vector3 startRef;

	[SerializeField]
	[HideInInspector]
	private Vector3 endRef;

	[SerializeField]
	[HideInInspector]
	private float offsetFromCenter;

	[HideInInspector]
	[SerializeField]
	private bool hasReferences;

	private void Start()
	{
		if (!hasReferences)
		{
			SetReferencePoints();
		}
	}

	public void SetReferencePoints()
	{
		Vector3 vector = base.transform.TransformPoint(startPoint);
		startRef = start.InverseTransformPoint(vector);
		Vector3 vector2 = base.transform.TransformPoint(endPoint);
		endRef = end.InverseTransformPoint(vector2);
		Vector3 position = (vector + vector2) * 0.5f;
		position = base.transform.InverseTransformPoint(position);
		position.y *= base.transform.lossyScale.y;
		offsetFromCenter = 0f - position.y;
		hasReferences = true;
	}

	private void LateUpdate()
	{
		if (!StatMaster.levelSimulating && !forceAlways)
		{
			return;
		}
		if (!StatMaster.isMP || (StatMaster.isClient && !StatMaster.isLocalSim))
		{
			if (start == null)
			{
				base.transform.parent = end;
				base.enabled = false;
				return;
			}
			if (end == null)
			{
				base.transform.parent = start;
				base.enabled = false;
				return;
			}
			Vector3 vector = start.TransformPoint(startRef);
			Vector3 vector2 = end.TransformPoint(endRef);
			Vector3 vector3 = (vector + vector2) * 0.5f;
			Vector3 forward = (start.forward + end.forward) * 0.5f;
			Vector3 normalized = (vector2 - vector).normalized;
			base.transform.position = vector3 + normalized * offsetFromCenter;
			base.transform.rotation = UpLookRotation(normalized, forward);
		}
		else
		{
			base.enabled = false;
		}
	}

	public static Quaternion UpLookRotation(Vector3 up, Vector3 forward)
	{
		return Quaternion.LookRotation(ClosestPerpendicular(up, forward), up);
	}

	public static Vector3 ClosestPerpendicular(Vector3 direction, Vector3 reference)
	{
		Vector3 vector = Vector3.Project(reference, direction);
		return (reference - vector).normalized;
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (Application.isPlaying)
		{
			if (start != null)
			{
				vector = start.TransformPoint(startRef);
			}
			if (end != null)
			{
				vector2 = end.TransformPoint(endRef);
			}
		}
		else
		{
			vector = base.transform.TransformPoint(startPoint);
			startRef = start.InverseTransformPoint(vector);
			vector2 = base.transform.TransformPoint(endPoint);
			endRef = end.InverseTransformPoint(vector2);
		}
		Vector3 vector3 = (vector + vector2) * 0.5f;
		Vector3 normalized = (vector2 - vector).normalized;
		DebugExtension.DebugWireSphere(vector3, (Color.yellow + Color.red) * 0.5f, 0.2f, 0f);
		DebugExtension.DebugWireSphere(vector3 + normalized * offsetFromCenter, Color.yellow, 0.2f, 0f);
		Debug.DrawLine(vector, vector2, Color.yellow);
	}
}
