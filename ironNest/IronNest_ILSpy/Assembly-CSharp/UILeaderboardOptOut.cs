using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILeaderboardOptOut : MonoBehaviour
{
	public Toggle Toggle;

	private void OnEnable()
	{
		//IL_00ae: Expected O, but got I4
		int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
		object obj = num - 1;
		bool isOnWithoutNotify = obj == null;
		Toggle.SetIsOnWithoutNotify(isOnWithoutNotify);
		Toggle toggle = Toggle;
		UnityAction<bool> call = OnToggleChanged;
		toggle.onValueChanged.RemoveListener(call);
		Toggle toggle2 = Toggle;
		UnityAction<bool> call2 = OnToggleChanged;
		toggle2.onValueChanged.AddListener(call2);
		NotifyListeners();
	}

	private void OnDisable()
	{
		Toggle toggle = Toggle;
		UnityAction<bool> call = OnToggleChanged;
		toggle.onValueChanged.RemoveListener(call);
	}

	private void OnToggleChanged(bool value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD24]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		PlayerPrefs.SetInt("LeaderboardOptOut", value ? 1 : 0);
		PlayerPrefs.Save();
		NotifyListeners();
	}

	private static void NotifyListeners()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		UILeaderboardOptOutListener[] array = Object.FindObjectsByType<UILeaderboardOptOutListener>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			((UILeaderboardOptOutListener)obj).UpdateVisibility();
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}
}
