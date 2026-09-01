using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionInfoDisplay : MonoBehaviour
{
	public TMP_Text Text_MissionName;

	public TMP_Text Text_MissionDescription;

	public Image Image_MapTopography;

	public UnityEvent OnPopulate;

	private MapCard _003CSourceCard_003Ek__BackingField;

	public MapCard SourceCard
	{
		get
		{
			return _003CSourceCard_003Ek__BackingField;
		}
		private set
		{
			_003CSourceCard_003Ek__BackingField = value;
		}
	}

	public void Populate(string missionName, string missionDescription, Sprite topographySprite, MapCard sourceCard)
	{
		MapCard mapCard = default(MapCard);
		_003CSourceCard_003Ek__BackingField = mapCard;
		if (OnPopulate != null)
		{
			OnPopulate.Invoke();
		}
		if (Text_MissionName != null)
		{
			Text_MissionName.text = missionName;
		}
		if (Text_MissionDescription != null)
		{
			Text_MissionDescription.text = missionDescription;
		}
		if (Image_MapTopography != null)
		{
			Image_MapTopography.sprite = topographySprite;
			bool flag = topographySprite != null;
			Image_MapTopography.enabled = flag;
		}
	}

	public void ActivateMission()
	{
		if (!(_003CSourceCard_003Ek__BackingField == null))
		{
			_003CSourceCard_003Ek__BackingField.ActivateMission();
		}
		else
		{
			Debug.LogWarning("[MissionInfoDisplay] ActivateMission called but no source MapCard is set. Ensure PopulateMissionInfo() has been called on a MapCard first.", this);
		}
	}

	public void Clear()
	{
		//IL_003e: Expected O, but got I
		//IL_004e: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00ab: Expected O, but got I
		_003CSourceCard_003Ek__BackingField = null;
		if (Text_MissionName != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rax_v21+B8]");
			object text = 0;
			Text_MissionName.text = (string)text;
		}
		if (Text_MissionDescription != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ rax_v15+B8]");
			object text2 = 0;
			Text_MissionDescription.text = (string)text2;
		}
		if (Image_MapTopography != null)
		{
			Image_MapTopography.sprite = null;
			Image_MapTopography.enabled = false;
		}
	}
}
