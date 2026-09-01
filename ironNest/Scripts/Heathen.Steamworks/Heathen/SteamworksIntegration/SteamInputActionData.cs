using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Input Action")]
	[HelpURL("https://heathen.group/kb/input/")]
	public class SteamInputActionData : MonoBehaviour, ISteamInputActionData
	{
		[SerializeField]
		private string setName;

		[SerializeField]
		private string layerName;

		[SerializeField]
		private string actionName;

		private InputActionSetData _mSet;

		private InputActionSetLayerData _mLayer;

		private InputActionData _mAction;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public InputActionSetData Set
		{
			get
			{
				return default(InputActionSetData);
			}
			set
			{
			}
		}

		public InputActionSetLayerData Layer
		{
			get
			{
				return default(InputActionSetLayerData);
			}
			set
			{
			}
		}

		public InputActionData Action
		{
			get
			{
				return default(InputActionData);
			}
			set
			{
			}
		}

		private void Start()
		{
		}

		private void Interface_OnReady()
		{
		}
	}
}
