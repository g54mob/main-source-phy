using UnityEngine;

[AddComponentMenu("Achievements/Other/AchievementTrophyPickup")]
internal class AchievementTrophyPickup : MonoBehaviour
{
	protected TrophyAchievementTrigger trigger;

	private BlockBehaviour block;

	[Header("Achievement")]
	public int level;

	[Header("Pickup Effect")]
	public GameObject praticleParent;

	public ParticleSystem[] particleSystems;

	public MeshRenderer vis;

	public MeshRenderer disabledVis;

	public static bool IsAvailable;

	public void Start()
	{
		trigger = LevelAchievementTrigger.levelAchievements[level] as TrophyAchievementTrigger;
		if (StatMaster.GodTools.HasBeenUsed || !StatMaster.Bounding.Enabled || (trigger.AchievementID != -1 && trigger.Completed()))
		{
			vis.enabled = false;
			disabledVis.enabled = true;
		}
	}

	public void OnEnable()
	{
		IsAvailable = true;
	}

	public void OnDisable()
	{
		IsAvailable = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (StatMaster.levelSimulating && collision.rigidbody != null)
		{
			GetBlock(collision.rigidbody);
		}
	}

	internal void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating && other.attachedRigidbody != null)
		{
			GetBlock(other.attachedRigidbody);
		}
	}

	private void GetBlock(Rigidbody rb)
	{
		block = rb.GetComponent<BlockBehaviour>();
		if (block != null && block.isSimulating)
		{
			if (StatMaster.GodTools.HasBeenUsed || !StatMaster.Bounding.Enabled)
			{
				GodToolsWarning.current.CheatsEnabled();
				base.gameObject.SetActive(false);
				return;
			}
			DisplayVisual();
			trigger.ExternalTrigger();
			base.gameObject.SetActive(false);
			base.enabled = false;
		}
	}

	private void DisplayVisual()
	{
		if (vis.enabled)
		{
			praticleParent.transform.SetParent(ReferenceMaster.physicsGoalInstance, true);
			for (int i = 0; i < particleSystems.Length; i++)
			{
				particleSystems[i].Play();
			}
		}
	}
}
