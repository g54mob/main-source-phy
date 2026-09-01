using UnityEngine;

public class EnableIfSteam : MonoBehaviour
{
	private void Start()
	{
		if (!SteamManager.Initialized)
		{
			base.gameObject.SetActive(false);
		}
	}
}
