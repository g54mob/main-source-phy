using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
	private Transform target;

	private UnitBowAnimation bowAnim;

	private Quaternion lookRotation;

	private Quaternion startRot;

	private void Start()
	{
		RangeWeapon componentInParent = base.transform.GetComponentInParent<RangeWeapon>();
		target = componentInParent.GetComponentInChildren<ArrowAimPos>().transform;
		bowAnim = componentInParent.GetComponentInChildren<UnitBowAnimation>();
		startRot = base.transform.localRotation;
	}

	private void Update()
	{
		if ((bool)bowAnim && bowAnim.drawn)
		{
			lookRotation = Quaternion.LookRotation(target.position - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, lookRotation, Time.deltaTime * 25f);
		}
	}

	public void ResetArrowRot()
	{
		base.transform.localRotation = startRot;
	}
}
