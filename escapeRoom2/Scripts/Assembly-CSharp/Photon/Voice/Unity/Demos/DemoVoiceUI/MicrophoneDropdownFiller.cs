using System.Collections.Generic;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
	public class MicrophoneDropdownFiller : MonoBehaviour
	{
		private List<MicRef> micOptions;

		[SerializeField]
		private Dropdown micDropdown;

		[SerializeField]
		private Recorder recorder;

		[SerializeField]
		[FormerlySerializedAs("RefreshButton")]
		private GameObject refreshButton;

		[SerializeField]
		[FormerlySerializedAs("ToggleButton")]
		private GameObject toggleButton;

		private Toggle photonToggle;

		private void Awake()
		{
			photonToggle = toggleButton.GetComponentInChildren<Toggle>();
			RefreshMicrophones();
		}

		private void OnEnable()
		{
			MicrophonePermission.MicrophonePermissionCallback += OnMicrophonePermissionCallback;
		}

		private void OnMicrophonePermissionCallback(bool granted)
		{
			RefreshMicrophones();
		}

		private void OnDisable()
		{
			MicrophonePermission.MicrophonePermissionCallback -= OnMicrophonePermissionCallback;
		}

		private void SetupMicDropdown()
		{
			micDropdown.ClearOptions();
			micOptions = new List<MicRef>();
			List<string> list = new List<string>();
			for (int i = 0; i < Microphone.devices.Length; i++)
			{
				string arg = Microphone.devices[i];
				micOptions.Add(new MicRef(arg));
				list.Add($"[Unity] {arg}");
			}
			if (recorder.MicrophonesEnumerator.IsSupported)
			{
				int num = 0;
				foreach (DeviceInfo item in recorder.MicrophonesEnumerator)
				{
					string arg2 = item.Name;
					micOptions.Add(new MicRef(arg2, item.IDInt));
					list.Add($"[Photon] {arg2}");
					num++;
				}
			}
			micDropdown.AddOptions(list);
			micDropdown.onValueChanged.RemoveAllListeners();
			micDropdown.onValueChanged.AddListener(delegate
			{
				MicDropdownValueChanged(micOptions[micDropdown.value]);
			});
		}

		private void MicDropdownValueChanged(MicRef mic)
		{
			recorder.MicrophoneType = mic.MicType;
			switch (mic.MicType)
			{
			case Recorder.MicType.Unity:
				recorder.UnityMicrophoneDevice = mic.Name;
				break;
			case Recorder.MicType.Photon:
				recorder.PhotonMicrophoneDeviceId = mic.PhotonId;
				break;
			}
			if (recorder.RequiresRestart)
			{
				recorder.RestartRecording();
			}
		}

		private void SetCurrentValue()
		{
			if (micOptions == null)
			{
				Debug.LogWarning("micOptions list is null");
				return;
			}
			bool isSupported = recorder.MicrophonesEnumerator.IsSupported;
			photonToggle.onValueChanged.RemoveAllListeners();
			photonToggle.isOn = recorder.MicrophoneType == Recorder.MicType.Photon;
			if (!isSupported)
			{
				photonToggle.onValueChanged.AddListener(PhotonMicToggled);
			}
			micDropdown.gameObject.SetActive(isSupported || recorder.MicrophoneType == Recorder.MicType.Unity);
			toggleButton.SetActive(!isSupported);
			refreshButton.SetActive(isSupported || recorder.MicrophoneType == Recorder.MicType.Unity);
			for (int i = 0; i < micOptions.Count; i++)
			{
				MicRef micRef = micOptions[i];
				if (recorder.MicrophoneType == micRef.MicType)
				{
					if (recorder.MicrophoneType == Recorder.MicType.Unity && Recorder.CompareUnityMicNames(micRef.Name, recorder.UnityMicrophoneDevice))
					{
						micDropdown.value = i;
						return;
					}
					if (recorder.MicrophoneType == Recorder.MicType.Photon && micRef.PhotonId == recorder.PhotonMicrophoneDeviceId)
					{
						micDropdown.value = i;
						return;
					}
				}
			}
			for (int j = 0; j < micOptions.Count; j++)
			{
				MicRef micRef2 = micOptions[j];
				if (recorder.MicrophoneType == micRef2.MicType)
				{
					if (recorder.MicrophoneType == Recorder.MicType.Unity)
					{
						micDropdown.value = j;
						recorder.UnityMicrophoneDevice = micRef2.Name;
						break;
					}
					if (recorder.MicrophoneType == Recorder.MicType.Photon)
					{
						micDropdown.value = j;
						recorder.PhotonMicrophoneDeviceId = micRef2.PhotonId;
						break;
					}
				}
			}
			if (recorder.RequiresRestart)
			{
				recorder.RestartRecording();
			}
		}

		public void PhotonMicToggled(bool on)
		{
			micDropdown.gameObject.SetActive(!on);
			refreshButton.SetActive(!on);
			if (on)
			{
				recorder.MicrophoneType = Recorder.MicType.Photon;
			}
			else
			{
				recorder.MicrophoneType = Recorder.MicType.Unity;
			}
			if (recorder.RequiresRestart)
			{
				recorder.RestartRecording();
			}
		}

		public void RefreshMicrophones()
		{
			recorder.MicrophonesEnumerator.Refresh();
			SetupMicDropdown();
			SetCurrentValue();
		}

		private void PhotonVoiceCreated()
		{
			RefreshMicrophones();
		}
	}
}
