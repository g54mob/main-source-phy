using System;
using UnityEngine;

[AddComponentMenu("Blocks/Physics/ExplodeOnTriggerEnter")]
public class ExplodeOnTriggerEnter : MonoBehaviour
{
	public Action<Rigidbody, bool> OnExplode;

	private void OnTriggerEnter(Collider col)
	{
		Explode(col);
	}

	public void Explode(Collider hit)
	{
		Rigidbody attachedRigidbody = hit.attachedRigidbody;
		if (attachedRigidbody == null || attachedRigidbody.CompareTag("KeepConstraintsAlways") || attachedRigidbody.CompareTag("Enemy"))
		{
			return;
		}
		GameObject go = attachedRigidbody.gameObject;
		int mask = 32;
		int mask2 = 141;
		bool arg = false;
		foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(go))
		{
			if (@interface.OnExplode(1000f, 1f, 1f, hit.transform.position, 1f, mask, base.transform.position.y < WaterController.waterTransformHeight))
			{
				arg = true;
			}
			else
			{
				@interface.OnExplode(1000f, 1f, 1f, hit.transform.position, 1f, mask2, base.transform.position.y < WaterController.waterTransformHeight);
			}
		}
		if (OnExplode != null)
		{
			OnExplode(attachedRigidbody, arg);
		}
	}
}
