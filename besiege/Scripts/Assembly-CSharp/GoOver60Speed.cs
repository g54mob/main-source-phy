using UnityEngine;

[AddComponentMenu("Achievements/Trigger/Generic/GoOver60Speed")]
internal class GoOver60Speed : AchievementTrigger
{
	private const float MinimumSpeed = 60f;

	internal override int AchievementId
	{
		get
		{
			return 2;
		}
	}

	public override void OnUpdate(int levelIndex)
	{
		if (StatMaster.isMP)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return;
		}
		BlockBehaviour firstBlock = machine.FirstBlock;
		if ((bool)firstBlock && !firstBlock.noRigidbody)
		{
			float magnitude = firstBlock.Rigidbody.velocity.magnitude;
			if (magnitude >= 60f)
			{
				Trigger();
			}
		}
	}
}
