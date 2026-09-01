using UnityEngine;

public class FPScounter : MonoBehaviour
{
	public TextMesh TextMeshT;

	private void UpdateFPSTextMesh()
	{
		float fPS = SingleInstance<PerformanceAnalyser>.Instance.FPS;
		TextMeshT.text = fPS.ToString("f2");
	}

	private void OnEnable()
	{
		if (!StatMaster.isHeadless)
		{
			InvokeRepeating("UpdateFPSTextMesh", 0.1f, 0.5f);
		}
	}

	private void OnDisable()
	{
		if (!StatMaster.isHeadless)
		{
			CancelInvoke("UpdateFPSTextMesh");
		}
	}
}
