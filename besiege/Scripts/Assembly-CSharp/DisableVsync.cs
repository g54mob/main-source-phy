using UnityEngine;

public class DisableVsync : MonoBehaviour
{
	private void Start()
	{
		QualitySettings.vSyncCount = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown("v"))
		{
			if (QualitySettings.vSyncCount == 0)
			{
				QualitySettings.vSyncCount = 1;
			}
			else
			{
				QualitySettings.vSyncCount = 0;
			}
		}
	}
}
