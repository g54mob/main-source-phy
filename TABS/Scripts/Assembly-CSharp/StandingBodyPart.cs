using UnityEngine;

public class StandingBodyPart : MonoBehaviour
{
	public StandinBodyPartData[] parts;

	public AnimationCurve forceCurve;

	public float offset;

	public float force;

	private DataHandler data;

	private void Start()
	{
		data = base.transform.GetComponentInParent<DataHandler>();
	}

	private void FixedUpdate()
	{
		if (data.Dead)
		{
			return;
		}
		float num = data.muscleControl * force;
		for (int i = 0; i < parts.Length; i++)
		{
			StandinBodyPartData standinBodyPartData = parts[i];
			if (!(standinBodyPartData.groundChecker.sinceGrounded > 0.1f))
			{
				float num2 = Mathf.Abs(standinBodyPartData.rig.transform.TransformPoint(standinBodyPartData.forcePoint).y - standinBodyPartData.groundChecker.groundPos.y);
				standinBodyPartData.rig.AddForce(forceCurve.Evaluate(num2 + offset) * num * Vector3.up, ForceMode.Acceleration);
			}
		}
	}
}
