using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class OutlineConnection : Connection<bool>
{
	private readonly string _targetTag;

	private readonly bool _resolveEverySet;

	private readonly bool _logWarnings;

	private OutlineController _cachedController;

	public OutlineConnection(string targetTag, bool resolveEverySet, bool logWarnings)
	{
		bool flag = string.IsNullOrWhiteSpace(targetTag);
		bool flag2 = !flag;
		string targetTag2 = targetTag;
		if (!flag2)
		{
			targetTag2 = "OutlineController";
		}
		_targetTag = targetTag2;
		_resolveEverySet = resolveEverySet;
		_logWarnings = logWarnings;
	}

	public new void Destroy()
	{
		_cachedController = null;
	}

	public override bool Get()
	{
		//IL_0092: Expected I4, but got O
		if (Application.isPlaying)
		{
			OutlineController outlineController = ResolveController(allowCache: true);
			if (outlineController != null)
			{
				if ((object)outlineController != null)
				{
					return outlineController.CurrentState;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return true;
	}

	public override void Set(bool uiValue)
	{
		if (Application.isPlaying)
		{
			bool allowCache = !_resolveEverySet;
			OutlineController outlineController = ResolveController(allowCache);
			if (outlineController != null)
			{
				outlineController.ChangeOutlinesState(uiValue);
				base.NotifyListenersIfChanged(uiValue);
			}
		}
	}

	private OutlineController ResolveController(bool allowCache)
	{
		if (allowCache && _cachedController != null)
		{
			return _cachedController;
		}
		string text;
		object message;
		if (!string.IsNullOrWhiteSpace(_targetTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(_targetTag);
			if (gameObject != null)
			{
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (!(obj == null))
					{
						_cachedController = (OutlineController)obj;
						return (OutlineController)obj;
					}
					if (!_logWarnings)
					{
						goto IL_023b;
					}
					string[] array = new string[5];
					if (array != null)
					{
						array[0] = "[OutlineConnection] GameObject '";
						string name = gameObject.name;
						array[1] = name;
						array[2] = "' (tag '";
						array[3] = _targetTag;
						array[4] = "') has no OutlineController component.";
						text = string.Concat(array);
						goto IL_01bf;
					}
				}
				return (OutlineController)(object)new NullReferenceException();
			}
			if (_logWarnings)
			{
				text = "[OutlineConnection] No GameObject found with tag '" + _targetTag + "'.";
				goto IL_01bf;
			}
		}
		else if (_logWarnings)
		{
			message = "[OutlineConnection] TargetTag is empty. Cannot resolve target.";
			goto IL_0264;
		}
		goto IL_023b;
		IL_0264:
		Debug.LogWarning(message);
		goto IL_023b;
		IL_023b:
		return null;
		IL_01bf:
		message = text;
		goto IL_0264;
	}

	private bool DefaultValue()
	{
		return true;
	}
}
