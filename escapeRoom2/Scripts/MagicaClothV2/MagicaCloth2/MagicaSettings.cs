using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaSettings")]
	[HelpURL("https://magicasoft.jp/en/mc2_settings_component/")]
	public class MagicaSettings : ClothBehaviour
	{
		public enum RefreshMode
		{
			OnAwake = 0,
			EveryFrame = 1,
			OnStart = 2,
			Manual = 3
		}

		public RefreshMode refreshMode;

		[Range(30f, 150f)]
		public int simulationFrequency = 90;

		[Range(1f, 5f)]
		public int maxSimulationCountPerFrame = 3;

		public MagicaManager.InitializationLocation initializationLocation;

		public TimeManager.UpdateLocation updateLocation;

		public bool monitorPlayerLoop;

		[Min(0f)]
		public int splitProxyMeshVertexCount = 300;

		public void Awake()
		{
			if (refreshMode == RefreshMode.OnAwake)
			{
				Refresh();
			}
		}

		public void Start()
		{
			if (refreshMode == RefreshMode.OnStart)
			{
				Refresh();
			}
		}

		public void Update()
		{
			if (refreshMode == RefreshMode.EveryFrame)
			{
				Refresh();
			}
		}

		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				Refresh();
			}
		}

		public void Refresh()
		{
			if (MagicaManager.IsPlaying())
			{
				simulationFrequency = Mathf.Clamp(simulationFrequency, 30, 150);
				maxSimulationCountPerFrame = Mathf.Clamp(maxSimulationCountPerFrame, 1, 5);
				MagicaManager.SetSimulationFrequency(simulationFrequency);
				MagicaManager.SetMaxSimulationCountPerFrame(maxSimulationCountPerFrame);
				MagicaManager.SetInitializationLocation(initializationLocation);
				MagicaManager.SetUpdateLocation(updateLocation);
				MagicaManager.SetSplitProxyMeshVertexCount(splitProxyMeshVertexCount);
				if (monitorPlayerLoop)
				{
					MagicaManager.InitCustomGameLoop();
				}
			}
			else
			{
				Develop.LogError((object)"MagicaManager is not starting!");
			}
		}
	}
}
