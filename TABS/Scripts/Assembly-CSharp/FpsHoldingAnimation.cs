using UnityEngine;

public class FpsHoldingAnimation : MonoBehaviour
{
	public AnimationCurve sideCurve;

	public AnimationCurve upCurve;

	public float animtionMultiplier = 1f;

	public float animationSpeed = 3f;

	private PlayerInput input;

	private FpsRangeWeaponHandler rangeWeapon;

	private float currentTime;

	private float multiplier;

	public float fadeInSpeed = 2f;

	private Transform cam;

	private void Start()
	{
		cam = base.transform.root.GetComponentInChildren<Camera>().transform;
		rangeWeapon = base.transform.root.GetComponentInChildren<FpsRangeWeaponHandler>();
		input = base.transform.root.GetComponentInChildren<PlayerInput>();
	}

	private void Update()
	{
		currentTime += Time.deltaTime * animationSpeed;
		if (currentTime >= 1f)
		{
			currentTime = 0f;
		}
		if (input.direction == Vector3.zero || rangeWeapon.isCharging)
		{
			multiplier = Mathf.Clamp(multiplier - Time.deltaTime * fadeInSpeed, 0f, 1f);
		}
		else
		{
			multiplier = Mathf.Clamp(multiplier + Time.deltaTime * fadeInSpeed, 0f, 1f);
		}
	}

	public Vector3 GetAnimationOffset()
	{
		Vector3 zero = Vector3.zero;
		float num = sideCurve.Evaluate(currentTime) * multiplier * animtionMultiplier;
		float num2 = upCurve.Evaluate(currentTime) * multiplier * animtionMultiplier;
		return zero + (cam.right * num + cam.up * num2);
	}
}
