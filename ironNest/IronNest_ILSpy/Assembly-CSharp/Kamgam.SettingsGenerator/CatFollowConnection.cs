using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CatFollowConnection : Connection<bool>
{
	private CatMovementManager catMovement;

	public override bool Get()
	{
		//IL_006a: Expected I4, but got O
		ResolveReferenceIfNeeded();
		if (catMovement != null)
		{
			CatMovementManager catMovementManager = catMovement;
			if ((object)catMovement != null)
			{
				return catMovementManager.EnabledCatFollow;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public override void Set(bool value)
	{
		ResolveReferenceIfNeeded();
		if (catMovement != null)
		{
			CatMovementManager catMovementManager = catMovement;
			catMovementManager.EnabledCatFollow = value;
			base.NotifyListenersIfChanged(value);
		}
	}

	private void ResolveReferenceIfNeeded()
	{
		if (catMovement == null)
		{
			CatMovementManager catMovementManager = UnityEngine.Object.FindAnyObjectByType<CatMovementManager>();
			catMovement = catMovementManager;
		}
	}
}
