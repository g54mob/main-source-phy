using UnityEngine;

public class MoveTransformVeloticyAfterCurve : MonoBehaviour
{
	private float counter;

	private MoveTransform moveTransform;

	private Vector3 originalVel;

	private float velMuliplier;

	public AnimationCurve velocitycurve;

	private void Awake()
	{
		moveTransform = GetComponent<MoveTransform>();
		if ((bool)moveTransform)
		{
			originalVel = moveTransform.velocity;
		}
		UpdateVel();
	}

	private void Update()
	{
		counter += Time.deltaTime;
		UpdateVel();
	}

	private void UpdateVel()
	{
		velMuliplier = velocitycurve.Evaluate(counter);
		moveTransform.UpdateVelMultiplier(velMuliplier);
	}
}
