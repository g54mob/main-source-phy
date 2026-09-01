using UnityEngine;

[AddComponentMenu("Destruction/Break On Target Collision")]
public class BreakOnTargetCollision : BreakBase
{
	public Rigidbody target;

	public GameObject BreakInto;

	public GameObject[] deactivate = new GameObject[0];

	public float forceToBreakSqr = 100f;

	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (!base.enabled || !base.SimPhysics || !base.isSimulating)
		{
			return;
		}
		float sqrMagnitude = collision.relativeVelocity.sqrMagnitude;
		Debug.Log(sqrMagnitude);
		if (sqrMagnitude > forceToBreakSqr)
		{
			Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;
			if (attachedRigidbody == target)
			{
				Break();
			}
		}
	}

	public virtual void Break()
	{
		if (!base.enabled)
		{
			return;
		}
		if (HasBasicInfo)
		{
			basicInfo.isDestroyed = true;
		}
		if (BreakInto == null)
		{
			Debug.LogWarning("BreakInto is null (" + Machine.GetObjectPath(base.gameObject) + ")!");
			return;
		}
		for (int i = 0; i < deactivate.Length; i++)
		{
			deactivate[i].SetActive(false);
		}
		GameObject gameObject = Object.Instantiate(BreakInto, base.transform.position, Quaternion.identity, base.transform.parent) as GameObject;
		if (gameObject == null)
		{
			OnBreak();
			return;
		}
		gameObject.SetActive(true);
		ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			if (StatMaster.isMP && NetworkBlock.applyingState)
			{
				Object.Destroy(componentsInChildren[j].gameObject);
			}
			else if (componentsInChildren[j].playOnAwake)
			{
				componentsInChildren[j].Stop();
				componentsInChildren[j].Clear();
				componentsInChildren[j].randomSeed = (uint)Random.Range(0, 9999999);
				componentsInChildren[j].Play();
			}
		}
		base.gameObject.SetActive(false);
		OnBreak();
	}
}
