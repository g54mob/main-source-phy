using UnityEngine;

public class WeaponAnimation : AttackEffect
{
	public string animationName = "";

	private Animator anim;

	public override void DoEffect(Rigidbody target, Vector3 targetDir)
	{
		if (!(animationName == ""))
		{
			if (!anim)
			{
				anim = base.transform.root.GetComponentInChildren<Animator>();
			}
			if ((bool)anim)
			{
				anim.Play(animationName);
			}
		}
	}
}
