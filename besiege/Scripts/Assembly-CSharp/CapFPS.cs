using UnityEngine;
using UnityEngine.SceneManagement;

public class CapFPS : MonoBehaviour
{
	public static CapFPS Instance;

	private void Awake()
	{
		Instance = this;
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	private void OnEnable()
	{
		SetTargetFrameRate(OptionsMaster.GetFPSLock());
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		SetTargetFrameRate(OptionsMaster.GetFPSLock());
	}

	public static void SetTargetFrameRate(int fps, bool force = false)
	{
		if (!StatMaster.SimulationStartInProgress || force)
		{
			Application.targetFrameRate = fps;
		}
	}
}
