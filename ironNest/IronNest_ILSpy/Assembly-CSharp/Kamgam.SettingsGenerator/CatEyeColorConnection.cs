using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CatEyeColorConnection : Connection<int>
{
	private CatCustomizationController catCustomization;

	public override int Get()
	{
		//IL_006a: Expected I4, but got O
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			CatCustomizationController catCustomizationController = catCustomization;
			if ((object)catCustomization != null)
			{
				return catCustomizationController.eyesColor;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	public override void Set(int value)
	{
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			catCustomization.ChangeEyesColor(value);
			base.NotifyListenersIfChanged(value);
		}
	}

	private void ResolveReferenceIfNeeded()
	{
		if (catCustomization == null)
		{
			CatCustomizationController catCustomizationController = UnityEngine.Object.FindAnyObjectByType<CatCustomizationController>();
			catCustomization = catCustomizationController;
		}
	}
}
