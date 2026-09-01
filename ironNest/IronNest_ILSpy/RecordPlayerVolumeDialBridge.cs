using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class RecordPlayerVolumeDialBridge : MonoBehaviour
{
	private RecordPlayerController recordPlayerController;

	private DialInteractable volumeDial;

	private bool debugLogs;

	private void Awake()
	{
		//IL_001d: Expected O, but got I4
		//IL_0069: Expected O, but got I4
		bool flag = recordPlayerController;
		object obj = 1;
		if (!flag)
		{
			string text = base.name;
			string message = "[RecordPlayerVolumeDialBridge] '" + text + "': 'recordPlayerController' is not assigned. Component will disable itself.";
			Debug.LogError(message, this);
			obj = 0;
		}
		if (!volumeDial)
		{
			string text2 = base.name;
			string message2 = "[RecordPlayerVolumeDialBridge] '" + text2 + "': 'volumeDial' is not assigned. Component will disable itself.";
			Debug.LogError(message2, this);
		}
		else if (obj != null)
		{
			return;
		}
		base.enabled = false;
	}

	private void OnEnable()
	{
		if ((bool)this.recordPlayerController && (bool)volumeDial)
		{
			RecordPlayerController recordPlayerController = this.recordPlayerController;
			volumeDial.SetDialValue(recordPlayerController.masterVolume);
			if (debugLogs)
			{
				string text = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text2 = $"Synced dial to current volume {arg:F3}.";
				string message = "[RecordPlayerVolumeDialBridge] '" + text + "': " + text2;
				Debug.Log(message, this);
			}
			DialInteractable dialInteractable = volumeDial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
		}
	}

	private void OnDisable()
	{
		if ((bool)volumeDial)
		{
			DialInteractable dialInteractable = volumeDial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
		}
	}

	private void HandleDialValueChanged(float value)
	{
		//IL_0036: Invalid comparison between I4 and F4
		//IL_0089: Expected F4, but got I4
		//IL_0097: Invalid comparison between I4 and F4
		//IL_00e7: Expected F4, but got I4
		if (!this.recordPlayerController)
		{
			return;
		}
		float num;
		if (!(0f > value))
		{
			bool flag = !(value > 1f);
			num = value;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		RecordPlayerController recordPlayerController = this.recordPlayerController;
		float masterVolume;
		if (!(0f > num))
		{
			bool flag2 = num > 1f;
			masterVolume = 1f;
			if (!flag2)
			{
				masterVolume = num;
			}
		}
		else
		{
			masterVolume = 0f;
		}
		recordPlayerController.masterVolume = masterVolume;
		recordPlayerController.ApplyMasterVolumeToSources();
		if (debugLogs)
		{
			string text = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text2 = $"Volume → {arg:F3}.";
			string message = "[RecordPlayerVolumeDialBridge] '" + text + "': " + text2;
			Debug.Log(message, this);
		}
	}
}
