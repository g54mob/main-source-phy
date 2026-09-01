using UnityEngine;

[AddComponentMenu("Achievements/Trigger/LevelSpecific/CageAchievementChecker")]
internal class CageAchievementChecker : MonoBehaviour
{
	public float targetDistance = 0.1f;

	public Transform gateTrack;

	[SerializeField]
	internal SpawnAchievementTrophy spawner;

	public Collider trigger;

	public HingeJoint joint;

	private void FixedUpdate()
	{
		if (IsClosed())
		{
			if (!trigger.enabled)
			{
				trigger.enabled = true;
			}
		}
		else if (trigger.enabled)
		{
			trigger.enabled = false;
		}
	}

	private bool IsClosed()
	{
		if (!StatMaster.levelSimulating)
		{
			return false;
		}
		if ((base.transform.position - gateTrack.position).sqrMagnitude < targetDistance)
		{
			return true;
		}
		return false;
	}

	private void OnTriggerEnter(Collider col)
	{
		Rigidbody attachedRigidbody = col.attachedRigidbody;
		if ((bool)attachedRigidbody && IsClosed() && attachedRigidbody.gameObject.CompareTag("ObjectiveObj"))
		{
			spawner.SpawnTrophy(base.transform.position);
			trigger.enabled = false;
			JointLimits limits = joint.limits;
			limits.max = limits.min + 1f;
			joint.limits = limits;
			base.enabled = false;
		}
	}
}
