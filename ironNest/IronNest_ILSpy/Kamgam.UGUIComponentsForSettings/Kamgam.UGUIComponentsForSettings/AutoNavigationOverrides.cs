using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class AutoNavigationOverrides : MonoBehaviour, ISelectHandler, IEventSystemHandler, IUpdateSelectedHandler
{
	protected Selectable selectable;

	public bool DisableOnAwakeIfNotNeeded = true;

	public Selectable SelectOnUpOverride;

	public Selectable SelectOnDownOverride;

	public Selectable SelectOnLeftOverride;

	public Selectable SelectOnRightOverride;

	public bool BlockUp;

	public bool BlockDown;

	public bool BlockLeft;

	public bool BlockRight;

	public Selectable Selectable
	{
		get
		{
			if (this.selectable == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Selectable selectable = default(Selectable);
				this.selectable = selectable;
			}
			return this.selectable;
		}
	}

	public bool IsBlockingAnyDirection
	{
		get
		{
			if (!BlockUp && !BlockDown && !BlockLeft)
			{
				return BlockRight;
			}
			return true;
		}
	}

	public void Awake()
	{
		Selectable selectable = Selectable;
		if (selectable != null)
		{
			Selectable selectable2 = Selectable;
			if ((nint)selectable2.m_Navigation == 4)
			{
				base.enabled = false;
			}
		}
		if (DisableOnAwakeIfNotNeeded)
		{
			bool flag = HasOverrides();
			if (!flag && BlockUp == flag && BlockDown == flag && BlockLeft == flag && BlockRight == flag)
			{
				base.enabled = false;
			}
		}
	}

	public bool HasOverrides()
	{
		if (SelectOnUpOverride == null && SelectOnDownOverride == null && SelectOnLeftOverride == null)
		{
			return SelectOnRightOverride != null;
		}
		return true;
	}

	public bool HasActiveOverrides()
	{
		//IL_01b8: Expected I4, but got O
		if (SelectOnUpOverride != null)
		{
			if ((object)SelectOnUpOverride == null)
			{
				goto IL_01aa;
			}
			if (SelectOnUpOverride.isActiveAndEnabled)
			{
				goto IL_0149;
			}
		}
		if (SelectOnDownOverride != null)
		{
			if ((object)SelectOnDownOverride == null)
			{
				goto IL_01aa;
			}
			if (SelectOnDownOverride.isActiveAndEnabled)
			{
				goto IL_0149;
			}
		}
		if (SelectOnLeftOverride != null)
		{
			if ((object)SelectOnLeftOverride == null)
			{
				goto IL_01aa;
			}
			if (SelectOnLeftOverride.isActiveAndEnabled)
			{
				goto IL_0149;
			}
		}
		bool flag = SelectOnRightOverride != null;
		if (!flag)
		{
			return flag;
		}
		if ((object)SelectOnRightOverride != null)
		{
			return SelectOnRightOverride.isActiveAndEnabled;
		}
		goto IL_01aa;
		IL_0149:
		return true;
		IL_01aa:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public void OnUpdateSelected(BaseEventData eventData)
	{
		ApplyOverrides();
	}

	public void ApplyOverrides()
	{
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		_ = 0;
		_ = 0;
		_ = 0;
		Selectable selectable = Selectable;
		if (!(selectable != null))
		{
			return;
		}
		object obj = default(object);
		if ((!(SelectOnUpOverride != null) || !SelectOnUpOverride.isActiveAndEnabled) && (!(SelectOnDownOverride != null) || !SelectOnDownOverride.isActiveAndEnabled) && (!(SelectOnLeftOverride != null) || !SelectOnLeftOverride.isActiveAndEnabled) && (!(SelectOnRightOverride != null) || !SelectOnRightOverride.isActiveAndEnabled) && !BlockUp && !BlockDown && !BlockLeft && !BlockRight)
		{
			Selectable selectable2 = Selectable;
			_ = selectable2.m_Navigation;
			_ = 3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ rax_v74 (UnityEngine.UI.Selectable)+38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ rax_v74 (UnityEngine.UI.Selectable)+48]");
			_ = 0;
		}
		else
		{
			Selectable selectable3 = Selectable;
			_ = selectable3.m_Navigation;
			_ = 3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rax_v16 (UnityEngine.UI.Selectable)+38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rax_v16 (UnityEngine.UI.Selectable)+48]");
			_ = 0;
			Selectable selectable4 = Selectable;
			Navigation navigation = (Navigation)(obj - 48);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-50]");
			_ = 0;
			selectable4.navigation = navigation;
			Selectable selectable5 = Selectable;
			Selectable selectable6 = selectable5.FindSelectableOnUp();
			Selectable selectable7 = Selectable;
			Selectable selectable8 = selectable7.FindSelectableOnDown();
			Selectable selectable9 = Selectable;
			Selectable selectable10 = selectable9.FindSelectableOnLeft();
			Selectable selectable11 = Selectable;
			Selectable selectable12 = selectable11.FindSelectableOnRight();
			Selectable selectable13 = Selectable;
			_ = selectable13.m_Navigation;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rax_v27 (UnityEngine.UI.Selectable)+38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rax_v27 (UnityEngine.UI.Selectable)+48]");
			_ = 0;
			_ = 4;
			if (HasOverrides())
			{
				if (SelectOnUpOverride != null)
				{
					Selectable selectOnUpOverride = SelectOnUpOverride;
					if (!selectOnUpOverride.m_Interactable)
					{
					}
				}
				if (SelectOnDownOverride != null)
				{
					Selectable selectOnDownOverride = SelectOnDownOverride;
					if (!selectOnDownOverride.m_Interactable)
					{
					}
				}
				if (SelectOnLeftOverride != null)
				{
					Selectable selectOnLeftOverride = SelectOnLeftOverride;
					if (!selectOnLeftOverride.m_Interactable)
					{
					}
				}
				if (SelectOnRightOverride != null)
				{
					Selectable selectOnRightOverride = SelectOnRightOverride;
					if (!selectOnRightOverride.m_Interactable)
					{
					}
				}
			}
			if (BlockUp)
			{
				_ = 0;
			}
			if (BlockDown)
			{
				_ = 0;
			}
			if (BlockLeft)
			{
				_ = 0;
			}
			if (BlockRight)
			{
				_ = 0;
			}
		}
		Selectable selectable14 = Selectable;
		Navigation navigation2 = (Navigation)(obj - 48);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-50]");
		_ = 0;
		selectable14.navigation = navigation2;
	}

	public void SetSelectableDown(Selectable selectable)
	{
		SelectOnDownOverride = selectable;
	}

	public void SetSelectableUp(Selectable selectable)
	{
		SelectOnUpOverride = selectable;
	}

	public void SetSelectableRight(Selectable selectable)
	{
		SelectOnRightOverride = selectable;
	}

	public void SetSelectableLeft(Selectable selectable)
	{
		SelectOnLeftOverride = selectable;
	}

	public Selectable FindSelectableOnUp()
	{
		//IL_015d: Expected I, but got O
		//IL_016d: Expected O, but got I
		//IL_017d: Expected O, but got I
		if (!BlockUp && !BlockDown && !BlockLeft && !BlockRight && SelectOnUpOverride != null)
		{
			if ((object)SelectOnUpOverride == null)
			{
				goto IL_018e;
			}
			if (SelectOnUpOverride.isActiveAndEnabled)
			{
				Selectable selectOnUpOverride = SelectOnUpOverride;
				if ((object)SelectOnUpOverride == null)
				{
					goto IL_018e;
				}
				if (selectOnUpOverride.m_Interactable)
				{
					goto IL_0187;
				}
			}
		}
		Selectable selectable = Selectable;
		if ((object)selectable != null)
		{
			nint num = (nint)selectable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+308]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+310]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v147 @ r8_v3 (should have been resolved before IL gen)");
			goto IL_0187;
		}
		goto IL_018e;
		IL_018e:
		return (Selectable)(object)new NullReferenceException();
		IL_0187:
		return SelectOnUpOverride;
	}

	public Selectable FindSelectableOnDown()
	{
		//IL_015d: Expected I, but got O
		//IL_016d: Expected O, but got I
		//IL_017d: Expected O, but got I
		if (!BlockUp && !BlockDown && !BlockLeft && !BlockRight && SelectOnDownOverride != null)
		{
			if ((object)SelectOnDownOverride == null)
			{
				goto IL_018e;
			}
			if (SelectOnDownOverride.isActiveAndEnabled)
			{
				Selectable selectOnDownOverride = SelectOnDownOverride;
				if ((object)SelectOnDownOverride == null)
				{
					goto IL_018e;
				}
				if (selectOnDownOverride.m_Interactable)
				{
					goto IL_0187;
				}
			}
		}
		Selectable selectable = Selectable;
		if ((object)selectable != null)
		{
			nint num = (nint)selectable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+318]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+320]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v147 @ r8_v3 (should have been resolved before IL gen)");
			goto IL_0187;
		}
		goto IL_018e;
		IL_018e:
		return (Selectable)(object)new NullReferenceException();
		IL_0187:
		return SelectOnDownOverride;
	}

	public Selectable FindSelectableOnLeft()
	{
		//IL_015d: Expected I, but got O
		//IL_016d: Expected O, but got I
		//IL_017d: Expected O, but got I
		if (!BlockUp && !BlockDown && !BlockLeft && !BlockRight && SelectOnLeftOverride != null)
		{
			if ((object)SelectOnLeftOverride == null)
			{
				goto IL_018e;
			}
			if (SelectOnLeftOverride.isActiveAndEnabled)
			{
				Selectable selectOnLeftOverride = SelectOnLeftOverride;
				if ((object)SelectOnLeftOverride == null)
				{
					goto IL_018e;
				}
				if (selectOnLeftOverride.m_Interactable)
				{
					goto IL_0187;
				}
			}
		}
		Selectable selectable = Selectable;
		if ((object)selectable != null)
		{
			nint num = (nint)selectable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+2E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+2F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v147 @ r8_v3 (should have been resolved before IL gen)");
			goto IL_0187;
		}
		goto IL_018e;
		IL_018e:
		return (Selectable)(object)new NullReferenceException();
		IL_0187:
		return SelectOnLeftOverride;
	}

	public Selectable FindSelectableOnRight()
	{
		//IL_015d: Expected I, but got O
		//IL_016d: Expected O, but got I
		//IL_017d: Expected O, but got I
		if (!BlockUp && !BlockDown && !BlockLeft && !BlockRight && SelectOnRightOverride != null)
		{
			if ((object)SelectOnRightOverride == null)
			{
				goto IL_018e;
			}
			if (SelectOnRightOverride.isActiveAndEnabled)
			{
				Selectable selectOnRightOverride = SelectOnRightOverride;
				if ((object)SelectOnRightOverride == null)
				{
					goto IL_018e;
				}
				if (selectOnRightOverride.m_Interactable)
				{
					goto IL_0187;
				}
			}
		}
		Selectable selectable = Selectable;
		if ((object)selectable != null)
		{
			nint num = (nint)selectable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+2F8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<UnityEngine.UI.Selectable>)+300]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v147 @ r8_v3 (should have been resolved before IL gen)");
			goto IL_0187;
		}
		goto IL_018e;
		IL_018e:
		return (Selectable)(object)new NullReferenceException();
		IL_0187:
		return SelectOnRightOverride;
	}

	public void OnSelect(BaseEventData eventData)
	{
		ApplyOverrides();
	}
}
