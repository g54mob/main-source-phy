using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Controller")]
	public class SteamInputControllerData : MonoBehaviour, ISteamInputControllerData
	{
		public enum ManagedEvents
		{
			Change = 0,
			Update = 1
		}

		[Tooltip("If set to true then we will attempt to force Steam to use input for this app on start.\nThis is generally only needed in editor testing.")]
		[SerializeField]
		private bool forceInput;

		[Tooltip("If true then the first controller connected will be set as the controller handle")]
		public bool getFirst;

		[FormerlySerializedAs("_delegates")]
		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<ManagedEvents> delegates;

		public UnityEvent onChanged;

		private InputHandle_t? _data;

		public InputHandle_t? Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		[ContextMenu("Set First Controller")]
		public void SetFirstFound()
		{
		}

		public InputMotionData_t GetMotion()
		{
			return default(InputMotionData_t);
		}

		private void HandleLateInit()
		{
		}
	}
}
