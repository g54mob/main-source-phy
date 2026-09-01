using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Utilities/MMSceneRestarter")]
	public class MMSceneRestarter : MonoBehaviour
	{
		public enum RestartModes
		{
			ActiveScene = 0,
			SpecificScene = 1
		}

		[Header("Settings")]
		public RestartModes RestartMode;

		[MMEnumCondition("RestartMode", new int[] { 1 })]
		public string SceneName;

		public LoadSceneMode LoadMode;

		[Header("Input")]
		public Key RestarterKey;

		protected string _newSceneName;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		public virtual void RestartScene()
		{
		}
	}
}
