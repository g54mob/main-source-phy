using UnityEngine;

public class BoulderController : MonoBehaviour
{
	private LayerMask mask = 553648128;

	private Rigidbody rb;

	private PlaySoundOnRoll soundController;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (soundController == null)
		{
			soundController = GetComponent<PlaySoundOnRoll>();
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (rb.IsSleeping())
		{
			rb.WakeUp();
		}
		if (soundController.WasGrounded())
		{
			return;
		}
		float num = 10f;
		RaycastHit hitInfo;
		if (!Physics.Raycast(base.transform.position, Vector3.down, out hitInfo, soundController.Radius + num + 2f, mask))
		{
			AtlasChallengeAchievement atlasChallengeAchievement = LevelAchievementTrigger.levelAchievements[WinCondition.Instance.myLevelIndex] as AtlasChallengeAchievement;
			if (!StatMaster.GodTools.HasBeenUsed && StatMaster.Bounding.Enabled && atlasChallengeAchievement.AchievementId != -1 && !atlasChallengeAchievement.Completed())
			{
				atlasChallengeAchievement.ExternalTrigger();
			}
		}
	}
}
