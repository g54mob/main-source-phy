using System;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class CreditsOpenListener : MonoBehaviour
{
	private CreditsPanel _creditsPanel;

	private EndOfMissionUIController _endOfMissionUIController;

	private void Start()
	{
		//IL_0085: Expected O, but got I4
		//IL_008e: Expected O, but got I4
		//IL_009c: Expected I, but got O
		EndOfMissionUIController endOfMissionUIController = _endOfMissionUIController;
		Action<MissionGraph> b = EndOfMissionUIController_OnMissionSummaryDisplayed;
		Delegate obj = Delegate.Combine(endOfMissionUIController.OnMissionSummaryDismissed, b);
		if ((object)obj == null)
		{
			endOfMissionUIController.OnMissionSummaryDismissed = (Action<MissionGraph>)obj;
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		Action<MissionGraph> action = default(Action<MissionGraph>);
		bool flag = action == null;
		object obj2 = 0;
		object obj3 = 0;
		nint num = (nint)typeof(Action<MissionGraph>);
		Delegate obj4 = obj;
		if (!flag)
		{
			endOfMissionUIController.OnMissionSummaryDismissed = action;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			if (obj5 != null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			object obj6 = default(object);
			obj2 = obj6;
			object obj7 = default(object);
			obj3 = obj7;
			IntPtr intPtr = default(IntPtr);
			num = intPtr;
			Delegate obj8 = default(Delegate);
			obj4 = obj8;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void OnDestroy()
	{
		//IL_0085: Expected O, but got I4
		//IL_008e: Expected O, but got I4
		//IL_009c: Expected I, but got O
		EndOfMissionUIController endOfMissionUIController = _endOfMissionUIController;
		Action<MissionGraph> value = EndOfMissionUIController_OnMissionSummaryDisplayed;
		Delegate obj = Delegate.Remove(endOfMissionUIController.OnMissionSummaryDismissed, value);
		if ((object)obj == null)
		{
			endOfMissionUIController.OnMissionSummaryDismissed = (Action<MissionGraph>)obj;
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		Action<MissionGraph> action = default(Action<MissionGraph>);
		bool flag = action == null;
		object obj2 = 0;
		object obj3 = 0;
		nint num = (nint)typeof(Action<MissionGraph>);
		Delegate obj4 = obj;
		if (!flag)
		{
			endOfMissionUIController.OnMissionSummaryDismissed = action;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			if (obj5 != null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			object obj6 = default(object);
			obj2 = obj6;
			object obj7 = default(object);
			obj3 = obj7;
			IntPtr intPtr = default(IntPtr);
			num = intPtr;
			Delegate obj8 = default(Delegate);
			obj4 = obj8;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void EndOfMissionUIController_OnMissionSummaryDisplayed(MissionGraph mission)
	{
		if (mission.ShowCreditsAfterSummary)
		{
			CreditsPanel creditsPanel = _creditsPanel;
			creditsPanel._isDisplayedFromMainMenu = false;
			GameObject gameObject = creditsPanel.gameObject;
			gameObject.SetActive(value: true);
			if (creditsPanel._onCreditsDisplayed != null)
			{
				creditsPanel._onCreditsDisplayed.Invoke();
			}
		}
	}
}
