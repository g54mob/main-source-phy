using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
	public class MMSceneLoadingAntiSpill
	{
		protected Scene _antiSpillScene;

		protected Scene _destinationScene;

		protected UnityAction<Scene, Scene> _onActiveSceneChangedCallback;

		protected string _sceneToLoadName;

		protected string _antiSpillSceneName;

		protected List<GameObject> _spillSceneRoots;

		protected static List<string> _scenesInBuild;

		public virtual void PrepareAntiFill(string sceneToLoadName, string antiSpillSceneName = "")
		{
		}

		protected virtual void PrepareAntiFillOnSceneLoaded(Scene newScene, LoadSceneMode mode)
		{
		}

		protected virtual void PrepareAntiFillSetSceneActive()
		{
		}

		protected virtual void OnActiveSceneChanged(Scene from, Scene to)
		{
		}

		protected virtual void EmptyAntiSpillScene()
		{
		}
	}
}
