using Cpp2ILInjected;
using UnityEngine;

public class NewspaperMusicRelay : MonoBehaviour
{
	public AudioClip AudioClip;

	public bool RestorePreviousMusic = true;

	public bool PlayOnEnable;

	private RecordPlayerController recordPlayerController;

	private bool warnedMissingController;

	private void OnEnable()
	{
		if (PlayOnEnable)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x1804458F0\"");
		}
	}

	public void PlayConfiguredMusic()
	{
		string text;
		string text2;
		if (!this.recordPlayerController)
		{
			RecordPlayerController recordPlayerController = Object.FindFirstObjectByType<RecordPlayerController>();
			this.recordPlayerController = recordPlayerController;
			bool flag = this.recordPlayerController;
			if (!flag)
			{
				if (warnedMissingController == flag)
				{
					warnedMissingController = true;
					text = base.name;
					text2 = "' could not find a RecordPlayerController.";
					goto IL_014c;
				}
				return;
			}
		}
		if ((bool)AudioClip)
		{
			string text3 = base.name;
			string message = "[NewspaperMusicRelay] '" + text3 + "' player newspaper music";
			Debug.Log(message, this);
			this.recordPlayerController.PlayNewspaperMusic(AudioClip, RestorePreviousMusic);
			return;
		}
		text = base.name;
		text2 = "' has no newspaper AudioClip assigned.";
		goto IL_014c;
		IL_014c:
		string message2 = "[NewspaperMusicRelay] '" + text + text2;
		Debug.LogWarning(message2, this);
	}

	private bool ResolveRecordPlayer()
	{
		if (!this.recordPlayerController)
		{
			RecordPlayerController recordPlayerController = Object.FindFirstObjectByType<RecordPlayerController>();
			this.recordPlayerController = recordPlayerController;
			bool flag = this.recordPlayerController;
			if (!flag)
			{
				if (warnedMissingController == flag)
				{
					warnedMissingController = true;
					string text = base.name;
					string message = "[NewspaperMusicRelay] '" + text + "' could not find a RecordPlayerController.";
					Debug.LogWarning(message, this);
				}
				return false;
			}
		}
		return true;
	}
}
