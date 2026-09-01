using UnityEngine;

[ExecuteInEditMode]
public class SetCenterOfMass : MonoBehaviour
{
	[HideInInspector]
	public Rigidbody body;

	public Vector3 center;

	private bool started;

	private void Awake()
	{
		started = true;
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			if (!Application.isPlaying)
			{
				body = GetComponent<Rigidbody>();
			}
			else
			{
				body.centerOfMass = Vector3.Scale(center, body.transform.lossyScale);
			}
		}
	}

	public void ScaleChanged()
	{
		if (started)
		{
			body.centerOfMass = Vector3.Scale(center, body.transform.lossyScale);
		}
	}
}
