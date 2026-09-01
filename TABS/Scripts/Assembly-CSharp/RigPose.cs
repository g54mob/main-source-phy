using UnityEngine;

public class RigPose : MonoBehaviour
{
	public Vector3 relativePosition = new Vector3(0f, 0.1f, 0.2f);

	public float force;

	private DataHandler data;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponentInParent<Rigidbody>();
		data = GetComponentInParent<DataHandler>();
		if ((bool)rig.GetComponent<HandLeft>())
		{
			relativePosition.x *= -1f;
		}
	}

	private void Update()
	{
		Vector3 vector = data.torso.TransformPoint(relativePosition) - rig.position;
		rig.AddForceAtPosition(60f * data.ragdollControl * force * Time.deltaTime * vector, rig.position, ForceMode.Acceleration);
	}
}
