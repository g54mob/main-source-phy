using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[HelpURL(null)]
	public class SteamInputManager : MonoBehaviour
	{
		public static SteamInputManager Current;

		[Tooltip("If set to true then we will attempt to force Steam to use input for this app on start.\nThis is generally only needed in editor testing.")]
		[SerializeField]
		private bool forceInput;

		[Tooltip("If set to true the system will update every input action every frame for every controller found")]
		public bool autoUpdate;

		[Header("Events")]
		public ControllerDataEvent onInputDataChanged;

		private bool _lastAutoUpdate;

		public static bool AutoUpdate
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public static List<InputControllerStateData> Controllers { get; private set; }

		private void Start()
		{
		}

		private void HandleInitialization()
		{
		}

		private void OnDestroy()
		{
		}

		private void LateUpdate()
		{
		}

		public void Refresh()
		{
		}
	}
}
