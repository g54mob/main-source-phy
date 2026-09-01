using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class GunLoadedShellIdToTMP3D : MonoBehaviour
{
	private GunController gun;

	private TextMeshPro targetText;

	private bool requireCanFire;

	private bool allowBlueprintOnChildren;

	private string fallbackTextForTMP;

	private bool updateEveryFrame;

	private string resolvedText;

	private bool autoFindGunOnValidate;

	private bool writeFallbackInEditMode;

	private string lastAppliedText;

	private void OnValidate()
	{
		if (autoFindGunOnValidate && gun == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			GunController gunController = default(GunController);
			gun = gunController;
		}
		bool isPlaying = Application.isPlaying;
		if (!isPlaying && writeFallbackInEditMode != isPlaying && targetText != null)
		{
			resolvedText = fallbackTextForTMP;
			targetText.text = resolvedText;
			lastAppliedText = resolvedText;
		}
	}

	private void Awake()
	{
		RefreshAndApply(forceApply: true);
	}

	private void Update()
	{
		if (Application.isPlaying)
		{
			RefreshAndApply(updateEveryFrame);
		}
	}

	private void RefreshAndApply(bool forceApply)
	{
		string text = ResolveShellIdOrFallback_Safe();
		resolvedText = text;
		if (targetText != null && (forceApply || lastAppliedText != resolvedText))
		{
			targetText.text = resolvedText;
			lastAppliedText = resolvedText;
		}
	}

	private string ResolveShellIdOrFallback_Safe()
	{
		//IL_00d8: Expected O, but got I
		//IL_0199: Expected O, but got I
		//IL_01bb: Expected O, but got I
		//IL_01d0: Expected O, but got I
		//IL_01e1: Expected O, but got I
		bool flag = gun == null;
		if (flag || (requireCanFire != flag && !gun.CanFire))
		{
			return fallbackTextForTMP;
		}
		GunController gunController = gun;
		if ((object)gun != null)
		{
			UnityEngine.Object artilleryReloadController = gunController.artilleryReloadController;
			string result;
			if (gunController.artilleryReloadController != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rsi_v8 (UnityEngine.Object)+38]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					bool flag2 = obj == null;
					bool flag3 = !flag2;
					UnityEngine.Object obj2 = obj;
					if (!flag3)
					{
						bool flag4 = !allowBlueprintOnChildren;
						obj2 = obj;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9210");
							UnityEngine.Object obj3 = default(UnityEngine.Object);
							obj2 = obj3;
						}
					}
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rdi_v13 (UnityEngine.Object)+30]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rdi_v13 (UnityEngine.Object)+30]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rdi_v16+18]");
							result = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rdi_v16+18]");
							if (string.IsNullOrEmpty((string)0))
							{
								result = fallbackTextForTMP;
							}
						}
						else
						{
							result = fallbackTextForTMP;
						}
					}
					else
					{
						result = fallbackTextForTMP;
					}
				}
				else
				{
					result = fallbackTextForTMP;
				}
			}
			else
			{
				result = fallbackTextForTMP;
			}
			return result;
		}
		return (string)(object)new NullReferenceException();
	}

	public GunLoadedShellIdToTMP3D()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39FF2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		requireCanFire = true;
		fallbackTextForTMP = "None";
		resolvedText = "None";
		autoFindGunOnValidate = true;
		base._002Ector();
	}
}
