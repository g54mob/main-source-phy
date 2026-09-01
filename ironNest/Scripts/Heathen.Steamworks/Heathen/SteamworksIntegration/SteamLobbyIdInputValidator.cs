using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[HelpURL("https://kb.heathen.group/steam/features/lobby/unity-lobby/steam-lobby-input-validator")]
	[RequireComponent(typeof(TMP_InputField))]
	public class SteamLobbyIdInputValidator : MonoBehaviour
	{
		[Header("Configuration")]
		public int minimalIdLength;

		[FormerlySerializedAs("OnValid")]
		[Header("Events")]
		public UnityEvent onValid;

		private TMP_InputField _mInputField;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleValueChanged(string arg0)
		{
		}
	}
}
