using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CatTypeConnection : Connection<bool>
{
	private CatCustomizationController catCustomization;

	public override bool Get()
	{
		//IL_006a: Expected I4, but got O
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			CatCustomizationController catCustomizationController = catCustomization;
			if ((object)catCustomization != null)
			{
				return catCustomizationController.isKitten;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public override void Set(bool value)
	{
		ResolveReferenceIfNeeded();
		if (catCustomization != null)
		{
			if (!value)
			{
				catCustomization.ChangeToAdultCat();
			}
			else
			{
				catCustomization.ChangeToKitten();
			}
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
