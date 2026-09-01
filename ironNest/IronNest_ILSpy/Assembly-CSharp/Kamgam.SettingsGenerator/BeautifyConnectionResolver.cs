using System;
using Beautify.Universal;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public class BeautifyConnectionResolver
{
	private Beautify.Universal.Beautify _cached;

	private readonly bool _resolveEveryAccess;

	private readonly bool _logWarnings;

	public BeautifyConnectionResolver(bool resolveEveryAccess, bool logWarnings)
	{
		_resolveEveryAccess = resolveEveryAccess;
		_logWarnings = logWarnings;
	}

	public void Invalidate()
	{
		_cached = null;
	}

	public Beautify.Universal.Beautify Resolve()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		if (!_resolveEveryAccess && _cached != null)
		{
			return _cached;
		}
		if (Application.isPlaying)
		{
			Volume[] array = UnityEngine.Object.FindObjectsByType<Volume>(FindObjectsSortMode.None);
			object obj = array + 32;
			Beautify.Universal.Beautify component = null;
			Beautify.Universal.Beautify beautify = null;
			Beautify.Universal.Beautify beautify2 = null;
			while ((nint)beautify2 < array.Length)
			{
				if ((nint)beautify < array.Length)
				{
					VolumeProfile profile = ((Volume)obj).profile;
					if (profile != null)
					{
						VolumeProfile profile2 = ((Volume)obj).profile;
						if (profile2.TryGet<Beautify.Universal.Beautify>(out component))
						{
							_cached = component;
							return component;
						}
					}
					beautify = (Beautify.Universal.Beautify)(beautify + 1);
					obj += 8;
					beautify2 = beautify;
					continue;
				}
				return (Beautify.Universal.Beautify)(object)new IndexOutOfRangeException();
			}
			if (_logWarnings)
			{
				Debug.LogWarning("[BeautifyConnection] No Volume with a Beautify override found in the scene. Add a Beautify override via Add Override → Kronnect → Beautify.");
			}
		}
		return null;
	}
}
