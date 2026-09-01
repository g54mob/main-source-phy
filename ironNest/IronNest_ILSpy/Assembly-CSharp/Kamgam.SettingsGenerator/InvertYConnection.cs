using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class InvertYConnection : Connection<bool>
{
	private readonly string _targetTag;

	private FirstPersonController _cachedController;

	public InvertYConnection(string targetTag)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		bool flag = string.IsNullOrWhiteSpace(targetTag);
		bool flag2 = !flag;
		string targetTag2 = targetTag;
		if (!flag2)
		{
			targetTag2 = "Player";
		}
		_targetTag = targetTag2;
	}

	public new void Destroy()
	{
		_cachedController = null;
	}

	public override bool Get()
	{
		//IL_008d: Expected I4, but got O
		if (Application.isPlaying)
		{
			FirstPersonController firstPersonController = ResolveController();
			if (firstPersonController != null)
			{
				if ((object)firstPersonController != null)
				{
					return firstPersonController.invertYCamera;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	public override void Set(bool uiValue)
	{
		if (Application.isPlaying)
		{
			FirstPersonController firstPersonController = ResolveController();
			if (firstPersonController != null)
			{
				firstPersonController.invertYCamera = uiValue;
				base.NotifyListenersIfChanged(uiValue);
			}
		}
	}

	private FirstPersonController ResolveController()
	{
		if (_cachedController == null)
		{
			if (!string.IsNullOrWhiteSpace(_targetTag))
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(_targetTag);
				if (gameObject != null)
				{
					if ((object)gameObject == null)
					{
						return (FirstPersonController)(object)new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (obj != null)
					{
						_cachedController = (FirstPersonController)obj;
						return (FirstPersonController)obj;
					}
				}
			}
			return null;
		}
		return _cachedController;
	}

	private bool DefaultValue()
	{
		return false;
	}
}
