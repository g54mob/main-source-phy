using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MouseClampingConnection : Connection<bool>
{
	private readonly string _targetTag;

	private readonly bool _resolveEverySet;

	private readonly bool _logWarnings;

	private DynamicCursorManager _cachedController;

	public MouseClampingConnection(string targetTag, bool resolveEverySet, bool logWarnings)
	{
		bool flag = string.IsNullOrWhiteSpace(targetTag);
		bool flag2 = !flag;
		string targetTag2 = targetTag;
		if (!flag2)
		{
			targetTag2 = "CursorManager";
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
			DynamicCursorManager dynamicCursorManager = ResolveController(allowCache: true);
			if (dynamicCursorManager != null)
			{
				if ((object)dynamicCursorManager != null)
				{
					return dynamicCursorManager.ClampMouseToValveSetting;
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
			DynamicCursorManager dynamicCursorManager = ResolveController(allowCache);
			if (dynamicCursorManager != null)
			{
				dynamicCursorManager.ClampMouseToValveSetting = uiValue;
				base.NotifyListenersIfChanged(uiValue);
			}
		}
	}

	private DynamicCursorManager ResolveController(bool allowCache)
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
						_cachedController = (DynamicCursorManager)obj;
						return (DynamicCursorManager)obj;
					}
					if (!_logWarnings)
					{
						goto IL_023b;
					}
					string[] array = new string[5];
					if (array != null)
					{
						array[0] = "[MouseClampingConnection] GameObject '";
						string name = gameObject.name;
						array[1] = name;
						array[2] = "' (tag '";
						array[3] = _targetTag;
						array[4] = "') has no DynamicCursorManager component.";
						text = string.Concat(array);
						goto IL_01bf;
					}
				}
				return (DynamicCursorManager)(object)new NullReferenceException();
			}
			if (_logWarnings)
			{
				text = "[MouseClampingConnection] No GameObject found with tag '" + _targetTag + "'.";
				goto IL_01bf;
			}
		}
		else if (_logWarnings)
		{
			message = "[MouseClampingConnection] TargetTag is empty. Cannot resolve target.";
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
