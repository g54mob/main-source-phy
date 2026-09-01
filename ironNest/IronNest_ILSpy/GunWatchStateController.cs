using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

public class GunWatchStateController : MonoBehaviour
{
	private ClipboardStateController _clipboardStateController;

	private Animator _animator;

	private static int IsHiddenParam;

	private static int IsRaisedParam;

	private ClipboardStateController.ClipboardState _lastClipboardState;

	private void Start()
	{
		//IL_0054: Expected O, but got I
		ClipboardStateController clipboardStateController = _clipboardStateController;
		List<ClipboardStateController.OverrideEntry> overrides = clipboardStateController._overrides;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		}
		ClipboardStateController.ClipboardState clipboardState = default(ClipboardStateController.ClipboardState);
		RefreshBasedOnClipboardState(ref clipboardState);
	}

	private void Update()
	{
		//IL_0071: Expected O, but got I
		//IL_0056: Expected O, but got I
		ClipboardStateController clipboardStateController = _clipboardStateController;
		List<ClipboardStateController.OverrideEntry> overrides = clipboardStateController._overrides;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		ClipboardStateController.ClipboardState clipboardState;
		object obj;
		if ((nint)0 <= (nint)0)
		{
			clipboardState = clipboardStateController._baseState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rdi_v1 (ClipboardStateController)+92]");
			obj = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj2 = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj3 = default(object);
			obj = obj3;
			ClipboardStateController.ClipboardState clipboardState2 = default(ClipboardStateController.ClipboardState);
			clipboardState = clipboardState2;
		}
		object obj4 = default(object);
		object obj5 = default(object);
		if ((object)clipboardState == (object)_lastClipboardState && obj4 == obj5)
		{
			object obj6 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GunWatchStateController)+32]");
			if (obj6 == null)
			{
				return;
			}
		}
		ClipboardStateController.ClipboardState clipboardState3 = default(ClipboardStateController.ClipboardState);
		RefreshBasedOnClipboardState(ref clipboardState3);
	}

	private void RefreshBasedOnClipboardState([In] ref ClipboardStateController.ClipboardState clipboardState)
	{
		//IL_0041: Expected O, but got I4
		//IL_005f: Expected O, but got I
		_lastClipboardState = clipboardState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [clipboardState @ rdx (ClipboardState&)+2]");
		_ = 0;
		Animator animator = _animator;
		int isHiddenParam = IsHiddenParam;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [clipboardState @ rdx (ClipboardState&)+2]");
		animator.SetBool(isHiddenParam, value: false);
		bool flag = (object)clipboardState != null;
		object obj = 1;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [clipboardState @ rdx (ClipboardState&)+1]");
			obj = 0;
		}
		bool flag2 = obj == null;
		bool value = !flag2;
		_animator.SetBool(IsRaisedParam, value);
	}

	static GunWatchStateController()
	{
		int isHiddenParam = Animator.StringToHash("IsHidden");
		IsHiddenParam = isHiddenParam;
		int isRaisedParam = Animator.StringToHash("IsRaised");
		IsRaisedParam = isRaisedParam;
	}
}
