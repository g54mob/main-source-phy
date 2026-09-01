using System;
using UnityEngine;

[AddComponentMenu("Physics/AI/LimbController")]
public class LimbController : MonoBehaviour
{
	public Action<LimbController> Severed;

	public float severThreshold;

	public ParticleSystem[] particles;

	public Transform content;

	[HideInInspector]
	public int index;

	public Transform refTransform;

	private Vector3 refPos;

	private void Start()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			refPos = refTransform.InverseTransformPoint(base.transform.position);
		}
	}

	public void OnCollisionEnter(Collision col)
	{
		if (StatMaster.levelSimulating && !(col.relativeVelocity.sqrMagnitude < severThreshold) && (bool)col.rigidbody)
		{
			BlockDamageType component = col.rigidbody.GetComponent<BlockDamageType>();
			if ((bool)component && component.DamageType == DamageType.Sharp)
			{
				Joint component2 = GetComponent<Joint>();
				UnityEngine.Object.Destroy(component2);
				OnJointBreak();
			}
		}
	}

	public void OnJointBreak()
	{
		if (Severed != null)
		{
			Severed(this);
		}
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
		Severed = null;
		UnityEngine.Object.Destroy(this);
	}

	public void LateUpdate()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim && (refTransform.TransformPoint(refPos) - base.transform.position).sqrMagnitude > 2f)
		{
			SeverLimb();
		}
	}

	private void SeverLimb()
	{
		content.parent = base.transform.parent;
		base.enabled = false;
	}
}
