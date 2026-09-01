using UnityEngine;

[AddComponentMenu("Achievements/Other/ClamBreak")]
internal class ClamBreak : BreakBase
{
	[SerializeField]
	internal SpawnAchievementTrophy spawner;

	public AudioSource sfx;

	public GameObject[] setLayer = new GameObject[0];

	private void OnJointBreak(float breakForce)
	{
		for (int i = 0; i < setLayer.Length; i++)
		{
			setLayer[i].layer = 0;
		}
		spawner.SpawnTrophy(base.transform.position);
		sfx.Play();
	}
}
