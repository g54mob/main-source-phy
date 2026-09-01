using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator
{
	public class LightDetector
	{
		public delegate void OnNewLightFoundDelegate(Light light);

		public static bool ScanAfterSceneLoad;

		public OnNewLightFoundDelegate OnNewLightFound;

		private static LightDetector _instance;

		protected List<Light> _lights;

		private List<GameObject> _tmpRootGameObjects;

		private List<Light> _tmpLights;

		public static LightDetector Instance => null;

		public List<Light> Lights => null;

		private LightDetector()
		{
		}

		private void onSceneLoaded(Scene scene, LoadSceneMode mode)
		{
		}

		public Light GetPrimaryLight()
		{
			return null;
		}

		public void Add(Light light)
		{
		}

		public void ScanAllScenes()
		{
		}

		public void ScanActiveScene()
		{
		}

		public void ScanScene(Scene scene)
		{
		}

		public void Defrag()
		{
		}
	}
}
