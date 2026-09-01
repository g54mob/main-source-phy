using UnityEngine;

[AddComponentMenu("Achievements/Other/DukesPleaAchievementProgress")]
internal class DukesPleaAchievementProgress : SpawnAchievementTrophy
{
	[Header("DukesPlea")]
	[SerializeField]
	internal GameObject[] diplomacySteps;

	[SerializeField]
	internal GameObject dukesDiplomat;

	internal override void Progress(MonoBehaviour b)
	{
		if (!base.enabled)
		{
			return;
		}
		if (b.gameObject.name == dukesDiplomat.name)
		{
			base.enabled = false;
			return;
		}
		if (diplomacySteps.Length - 1 > progress)
		{
			diplomacySteps[progress].SetActive(true);
		}
		progress++;
		if (progress == requiredBreaks)
		{
			SpawnTrophy(Vector3.zero);
		}
	}
}
