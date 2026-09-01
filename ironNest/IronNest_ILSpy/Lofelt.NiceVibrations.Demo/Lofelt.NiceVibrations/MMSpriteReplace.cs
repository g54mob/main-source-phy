using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMSpriteReplace : MonoBehaviour
{
	public Sprite OnSprite;

	public Sprite OffSprite;

	public bool StartsOn = true;

	protected Image _image;

	protected SpriteRenderer _spriteRenderer;

	protected MMTouchButton _mmTouchButton;

	public bool CurrentValue
	{
		get
		{
			//IL_0035: Expected I4, but got O
			Image image = _image;
			if ((object)_image != null)
			{
				return image.m_Sprite == OnSprite;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	protected virtual void Start()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+188]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+190]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void Initialization()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Image image = default(Image);
		_image = image;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		SpriteRenderer spriteRenderer = default(SpriteRenderer);
		_spriteRenderer = spriteRenderer;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		MMTouchButton mmTouchButton = default(MMTouchButton);
		_mmTouchButton = mmTouchButton;
		if (_mmTouchButton != null)
		{
			MMTouchButton mmTouchButton2 = _mmTouchButton;
			mmTouchButton2._003CReturnToInitialSpriteAutomatically_003Ek__BackingField = false;
		}
		if (!(OnSprite != null) || !(OffSprite != null))
		{
			return;
		}
		if (_image != null)
		{
			Sprite sprite = (StartsOn ? OnSprite : OffSprite);
			_image.sprite = sprite;
		}
		if (_spriteRenderer != null)
		{
			if (!StartsOn)
			{
				_spriteRenderer.sprite = OffSprite;
			}
			else
			{
				_spriteRenderer.sprite = OnSprite;
			}
		}
	}

	public virtual void Swap()
	{
		//IL_005f: Expected I, but got O
		//IL_010c: Expected I, but got O
		//IL_007d: Expected O, but got I
		//IL_008d: Expected O, but got I
		//IL_0135: Expected O, but got I
		//IL_0145: Expected O, but got I
		//IL_00a2: Expected O, but got I
		//IL_00b2: Expected O, but got I
		while (true)
		{
			if (_image != null)
			{
				Image image = _image;
				bool flag = image.m_Sprite == OnSprite;
				nint num = (nint)this;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1A8]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1B0]");
					object obj2 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1C8]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1D0]");
					object obj2 = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v247 @ rax_v21 (should have been resolved before IL gen)");
			}
			if (_spriteRenderer != null)
			{
				Sprite sprite = _spriteRenderer.sprite;
				bool flag2 = sprite != OnSprite;
				nint num2 = (nint)this;
				if (!flag2)
				{
					SwitchToOffSprite();
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rdx_v7 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rdx_v7 (Il2CppClass<Lofelt.NiceVibrations.MMSpriteReplace>)+1D0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v195 @ rax_v11 (should have been resolved before IL gen)");
				continue;
			}
			break;
		}
	}

	public virtual void SwitchToOffSprite()
	{
		if ((!(_image == null) || _spriteRenderer != null) && OffSprite != null)
		{
			SpriteOff();
		}
	}

	protected virtual void SpriteOff()
	{
		if (_image != null)
		{
			_image.sprite = OffSprite;
		}
		if (_spriteRenderer != null)
		{
			_spriteRenderer.sprite = OffSprite;
		}
	}

	public virtual void SwitchToOnSprite()
	{
		if ((!(_image == null) || _spriteRenderer != null) && OnSprite != null)
		{
			SpriteOn();
		}
	}

	protected virtual void SpriteOn()
	{
		if (_image != null)
		{
			_image.sprite = OnSprite;
		}
		if (_spriteRenderer != null)
		{
			_spriteRenderer.sprite = OnSprite;
		}
	}
}
