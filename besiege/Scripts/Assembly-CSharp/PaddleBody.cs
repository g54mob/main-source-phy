using UnityEngine;

[AddComponentMenu("Physics/Paddle Body")]
public class PaddleBody : MonoBehaviour
{
	public BasicInfo basicInfo;

	public Vector3 paddleAxis;

	private Vector3 currentVelocity;

	private Vector3 xyz;

	private bool tipInWater;

	private Vector3 tipPos = new Vector3(0f, 0f, 1.4f);

	private Vector3 basePos = new Vector3(0f, 0f, -0.5f);

	public void FixedUpdate()
	{
		if (!basicInfo.isSimulating)
		{
			return;
		}
		Vector3 vector = base.transform.TransformPoint(tipPos);
		tipInWater = WaterController.Exist && WaterController.IsUnderwater(vector);
		if (basicInfo.InWater || tipInWater)
		{
			currentVelocity = basicInfo.Rigidbody.GetPointVelocity(vector);
			xyz = -base.transform.InverseTransformDirection(currentVelocity);
			xyz = Vector3.Scale(xyz, paddleAxis);
			xyz = base.transform.TransformDirection(xyz);
			xyz = Vector3.Project(xyz, currentVelocity.normalized);
			xyz = Vector3.ClampMagnitude(xyz, 1000f);
			float num = basicInfo.submergedPercent;
			if (tipInWater && num < 0.5f)
			{
				num = 0.5f;
			}
			basicInfo.Rigidbody.AddForceAtPosition(xyz * num * 2f, base.transform.TransformPoint(basePos), ForceMode.Acceleration);
		}
	}

	public void OnJointBreak()
	{
		tipPos = (basePos = new Vector3(0f, 0f, 0.875f));
	}
}
