using UnityEngine;

public class RaiseWarMachine : MonoBehaviour
{
	public AnimationCurve localYPerRange;

	public float heightDif = 5f;

	public Transform forwardTransform;

	public float multiplier = 1f;

	private DataHandler data;

	private ConfigurableJoint joint;

	private void Start()
	{
		data = GetComponentInParent<DataHandler>();
		joint = GetComponent<ConfigurableJoint>();
	}

	private void Update()
	{
		float time = 45f;
		if ((bool)data.targetData)
		{
			time = Mathf.Clamp(data.distanceToTarget, 0f, 100f);
			time += forwardTransform.InverseTransformPoint(data.targetData.mainRig.position).y * heightDif;
		}
		if ((bool)joint)
		{
			joint.targetPosition = new Vector3(localYPerRange.Evaluate(time) * multiplier, 0f, 0f);
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(base.transform.localPosition.x, localYPerRange.Evaluate(time), base.transform.localPosition.z), Time.deltaTime * 2f);
		}
	}
}
