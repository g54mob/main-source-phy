using Cpp2ILInjected;
using UnityEngine;

public class UILeaderboardOptOutListener : MonoBehaviour
{
	private bool disabledByOptOut;

	private void Awake()
	{
		UpdateVisibility();
	}

	public void UpdateVisibility()
	{
		bool flag = LeaderboardManager.Instance == null;
		if (flag)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
		if ((nint)0 == (flag ? 1 : 0))
		{
			_ = 1;
		}
		int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
		if (num == 1)
		{
			GameObject gameObject = base.gameObject;
			if (gameObject.activeSelf)
			{
				disabledByOptOut = true;
				GameObject gameObject2 = base.gameObject;
				gameObject2.SetActive(value: false);
			}
		}
		else if (disabledByOptOut)
		{
			disabledByOptOut = false;
			GameObject gameObject3 = base.gameObject;
			gameObject3.SetActive(value: true);
		}
	}
}
