using System.Collections;
using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Look At")]
public class SmoothLookAtMachine : MonoBehaviour
{
	public Transform target;

	public Transform source;

	public bool getStartingBlock;

	public float checkTargetFrequency = 2f;

	public float wait = 2f;

	public float damping = 6f;

	public bool smooth = true;

	public bool onlyInSim;

	public float maxDistance = 1000f;

	private float sqrDistance;

	private bool settingUp = true;

	private float timer;

	public bool Simulating
	{
		get
		{
			return onlyInSim && StatMaster.levelSimulating;
		}
	}

	protected void LateUpdate()
	{
		if (!Simulating || settingUp)
		{
			return;
		}
		if (target == null)
		{
			timer += Time.deltaTime;
			if (timer > checkTargetFrequency)
			{
				GetNewTarget();
				timer = 0f;
			}
			return;
		}
		timer = 0f;
		if ((target.position - base.transform.position).sqrMagnitude > sqrDistance)
		{
			target = null;
		}
		else if (smooth)
		{
			Quaternion b = Quaternion.LookRotation(target.position - source.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
		}
		else
		{
			base.transform.LookAt(target);
		}
	}

	protected IEnumerator Start()
	{
		if (source == null)
		{
			source = base.transform;
		}
		if (Simulating)
		{
			if (onlyInSim)
			{
				yield return new WaitForSeconds(wait);
			}
			Rigidbody r = GetComponent<Rigidbody>();
			if ((bool)r)
			{
				r.freezeRotation = true;
			}
			GetNewTarget();
			sqrDistance = maxDistance * maxDistance;
			settingUp = false;
		}
	}

	public void GetNewTarget()
	{
		target = ((!getStartingBlock) ? ReferenceMaster.GetBlockWithinProximity(Machine.Active(), base.transform, maxDistance).SimBlock.transform : GameObject.Find("StartingBlock").transform);
	}
}
