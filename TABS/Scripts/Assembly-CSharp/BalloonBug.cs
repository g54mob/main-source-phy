using Landfall.TABS.GameMode;
using UnityEngine;

public class BalloonBug : MonoBehaviour
{
	public MeshRenderer[] rends;

	public Material mat;

	private void Awake()
	{
		bool flag = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
		if (Bugs._DLC_ACTIVATED && !flag && ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("BUG_METAL_BALLOON").currentValue == 1 && rends != null)
		{
			for (int i = 0; i < rends.Length; i++)
			{
				rends[i].sharedMaterial = mat;
			}
			GetComponent<SetTeamColorOnStart>().enabled = false;
			GetComponentInChildren<ConstantForce>().force *= -0.5f;
			GetComponent<PlaySoundEffect>().SoundRef = "Bugs/BalloonInflate";
		}
	}
}
