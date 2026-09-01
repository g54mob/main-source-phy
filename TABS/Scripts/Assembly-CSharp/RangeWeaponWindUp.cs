using UnityEngine;

public class RangeWeaponWindUp : MonoBehaviour
{
	public AnimationCurve fireRateCurve;

	public float allowedToFireThreshold;

	[HideInInspector]
	public float currentWindup;

	public float windupRemovedPerSecond = 1f;

	public float rotationPerWindup;

	public Transform rotationTransform;

	public Transform anotherTransform;

	private float maxWindup;

	private RangeWeapon rangeWeapon;

	private float sinceAttack = 1f;

	private void Start()
	{
		rangeWeapon = GetComponent<RangeWeapon>();
		rangeWeapon.AddAttackCallRecievedAction(Fire);
		maxWindup = fireRateCurve.keys[fireRateCurve.keys.Length - 1].time;
	}

	private void Update()
	{
		if (sinceAttack > 1f)
		{
			currentWindup -= windupRemovedPerSecond * Time.deltaTime;
		}
		if (sinceAttack < 1f)
		{
			currentWindup += Time.deltaTime;
		}
		currentWindup = Mathf.Clamp(currentWindup, 0f, maxWindup);
		sinceAttack += Time.deltaTime;
		rangeWeapon.allowedToFire = currentWindup > allowedToFireThreshold;
		rangeWeapon.transform.root.GetComponentInChildren<WeaponHandler>().attackSpeedMultiplier = 1f / fireRateCurve.Evaluate(currentWindup);
		rotationTransform.Rotate(Vector3.forward * Time.deltaTime * currentWindup * rotationPerWindup, Space.Self);
		if ((bool)anotherTransform)
		{
			anotherTransform.Rotate(Vector3.forward * Time.deltaTime * currentWindup * rotationPerWindup, Space.Self);
		}
	}

	public void Fire()
	{
		sinceAttack = 0f;
	}
}
