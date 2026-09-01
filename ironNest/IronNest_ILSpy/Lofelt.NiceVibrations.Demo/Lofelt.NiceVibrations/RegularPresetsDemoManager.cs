using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class RegularPresetsDemoManager : DemoManager
{
	private sealed class _003CChangeImageCoroutine_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RegularPresetsDemoManager _003C_003E4__this;

		public Sprite newSprite;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CChangeImageCoroutine_003Ed__17(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_021e: Expected I4, but got I8
			//IL_02de: Expected I4, but got O
			//IL_0039: Expected O, but got I4
			//IL_0195: Expected I4, but got I8
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_0104: Expected I4, but got I8
			//IL_0092: Expected I4, but got I8
			RegularPresetsDemoManager regularPresetsDemoManager = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this == null || (object)regularPresetsDemoManager.IconImage == null)
							{
								goto IL_02d0;
							}
							regularPresetsDemoManager.IconImage.sprite = regularPresetsDemoManager.IdleSprite;
						}
						return false;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null && (object)regularPresetsDemoManager.IconImageAnimator != null)
					{
						regularPresetsDemoManager.IconImageAnimator.SetBool(regularPresetsDemoManager._idleAnimationParameter, value: true);
						_003C_003E2__current = regularPresetsDemoManager._turnDelay;
						_003C_003E1__state = 3;
						return true;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null && (object)regularPresetsDemoManager.IconImage != null)
					{
						regularPresetsDemoManager.IconImage.sprite = newSprite;
						_003C_003E2__current = regularPresetsDemoManager._shakeDelay;
						_003C_003E1__state = 2;
						return true;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && (object)regularPresetsDemoManager.DebugAudioEmphasis != null)
				{
					regularPresetsDemoManager.DebugAudioEmphasis.Play();
					if ((object)regularPresetsDemoManager.IconImageAnimator != null)
					{
						regularPresetsDemoManager.IconImageAnimator.SetBool(regularPresetsDemoManager._idleAnimationParameter, value: false);
						_003C_003E2__current = regularPresetsDemoManager._turnDelay;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			goto IL_02d0;
			IL_02d0:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public Image IconImage;

	public Animator IconImageAnimator;

	public Sprite IdleSprite;

	public Sprite SelectionSprite;

	public Sprite SuccessSprite;

	public Sprite WarningSprite;

	public Sprite FailureSprite;

	public Sprite RigidSprite;

	public Sprite SoftSprite;

	public Sprite LightSprite;

	public Sprite MediumSprite;

	public Sprite HeavySprite;

	protected WaitForSeconds _turnDelay;

	protected WaitForSeconds _shakeDelay;

	protected int _idleAnimationParameter;

	protected virtual void Awake()
	{
		WaitForSeconds turnDelay = new WaitForSeconds(0.02f);
		_turnDelay = turnDelay;
		WaitForSeconds shakeDelay = new WaitForSeconds(0.3f);
		_shakeDelay = shakeDelay;
		int id = (_idleAnimationParameter = Animator.StringToHash("Idle"));
		IconImageAnimator.SetBool(id, value: true);
		IconImageAnimator.speed = 2f;
	}

	protected virtual void ChangeImage(Sprite newSprite)
	{
		IEnumerator routine = ChangeImageCoroutine(newSprite);
		Coroutine coroutine = StartCoroutine(routine);
	}

	protected virtual IEnumerator ChangeImageCoroutine(Sprite newSprite)
	{
		_003CChangeImageCoroutine_003Ed__17 obj = new _003CChangeImageCoroutine_003Ed__17(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.newSprite = newSprite;
		return obj;
	}

	public virtual void SelectionButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
			nint num = (nint)this;
			Sprite selectionSprite = SelectionSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void SuccessButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
			nint num = (nint)this;
			Sprite successSprite = SuccessSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void WarningButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
			nint num = (nint)this;
			Sprite warningSprite = WarningSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void FailureButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
			nint num = (nint)this;
			Sprite failureSprite = FailureSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void RigidButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
			nint num = (nint)this;
			Sprite rigidSprite = RigidSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void SoftButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
			nint num = (nint)this;
			Sprite softSprite = SoftSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void LightButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
			nint num = (nint)this;
			Sprite lightSprite = LightSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void MediumButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
			nint num = (nint)this;
			Sprite mediumSprite = MediumSprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public virtual void HeavyButton()
	{
		//IL_0014: Expected I, but got O
		//IL_002e: Expected O, but got I
		//IL_003e: Expected O, but got I
		while (true)
		{
			HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
			nint num = (nint)this;
			Sprite heavySprite = HeavySprite;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+188]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r8_v1 (Il2CppClass<Lofelt.NiceVibrations.RegularPresetsDemoManager>)+190]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v46 @ rax_v4 (should have been resolved before IL gen)");
		}
	}
}
