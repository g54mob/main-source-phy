using UnityEngine;

[ExecuteInEditMode]
public class BetweenWaterBobs : MonoBehaviour
{
	public Transform pivot1;

	public Transform pivot2;

	public bool onlyInBuildMode = true;

	public bool useSkewParent;

	[HideInInspector]
	public Vector3 startDir = Vector3.zero;

	[HideInInspector]
	public Vector3 offset = Vector3.zero;

	[HideInInspector]
	public Vector3 startForward = Vector3.zero;

	[Range(0f, 1f)]
	public float smooth = 0.5f;

	[Range(0f, 1f)]
	public float intensity = 1f;

	private bool isSelected;

	private BasicInfo info1;

	private BasicInfo info2;

	protected void Start()
	{
		if (Application.isPlaying)
		{
			info1 = pivot1.GetComponent<BasicInfo>();
			info2 = pivot2.GetComponent<BasicInfo>();
			if (StatMaster.levelSimulating && onlyInBuildMode)
			{
				Object.Destroy(this);
			}
		}
	}

	protected void Update()
	{
		if (!StatMaster.levelSimulating || !onlyInBuildMode)
		{
			BetweenPoints(pivot1.position, pivot2.position);
		}
		if (StatMaster.levelSimulating && WaterController.Exist && info1.submergedPercent + info2.submergedPercent < 0.5f && (info1.Rigidbody.velocity.y > 3f || info2.Rigidbody.velocity.y > 3f))
		{
			base.transform.GetChild(0).SendMessage("ExternalBreak", SendMessageOptions.DontRequireReceiver);
		}
	}

	protected virtual void BetweenPoints(Vector3 start, Vector3 end)
	{
		float num = end.x - start.x;
		float num2 = end.y - start.y;
		float num3 = end.z - start.z;
		float t = Mathf.Sqrt(smooth);
		Vector3 a = new Vector3(num, num2, num3);
		a = Vector3.Lerp(a, base.transform.forward, smooth);
		a.y = Mathf.Lerp(startForward.y, a.y, intensity);
		Vector3 vector = new Vector3(start.x + num * 0.5f, start.y + num2 * 0.5f, start.z + num3 * 0.5f);
		Vector3 position = vector + offset;
		position.y = Mathf.Lerp(position.y, base.transform.position.y, t);
		if (useSkewParent)
		{
			Transform parent = base.transform.parent;
			parent.forward = new Vector3(a.x, 0f, a.z);
			parent.position = vector;
		}
		base.transform.forward = a;
		base.transform.position = position;
	}

	private void UpdatePivot()
	{
		Vector3 vector = (pivot1.position + pivot2.position) / 2f;
		offset = base.transform.position - vector;
		offset = base.transform.rotation * offset;
		offset.x = (offset.z = 0f);
		offset = Quaternion.Inverse(base.transform.rotation) * offset;
		startForward = base.transform.forward;
		startForward.y = 0f;
		startForward = startForward.normalized;
	}
}
