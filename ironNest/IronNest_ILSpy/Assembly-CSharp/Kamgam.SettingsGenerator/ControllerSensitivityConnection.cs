using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ControllerSensitivityConnection : Connection<float>
{
	private readonly Vector2 _inputRange;

	private readonly string _targetTag;

	private readonly bool _resolveEverySet;

	private readonly bool _logWarnings;

	private ControllerSensitivitySetter _cachedSetter;

	public ControllerSensitivityConnection(Vector2 inputRange, string targetTag, bool resolveEverySet, bool logWarnings)
	{
		object obj = default(object);
		bool flag = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref inputRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		Vector2 inputRange2 = inputRange;
		if (!flag)
		{
			Vector2 vector = default(Vector2);
			inputRange2 = vector;
		}
		_inputRange = inputRange2;
		bool flag2 = string.IsNullOrWhiteSpace(targetTag);
		bool flag3 = !flag2;
		string targetTag2 = targetTag;
		if (!flag3)
		{
			targetTag2 = "ControllerSensitivity";
		}
		_targetTag = targetTag2;
		_resolveEverySet = resolveEverySet;
		bool logWarnings2 = default(bool);
		_logWarnings = logWarnings2;
	}

	public new void Destroy()
	{
		_cachedSetter = null;
	}

	public override float Get()
	{
		if (Application.isPlaying)
		{
			ControllerSensitivitySetter controllerSensitivitySetter = ResolveSetter(allowCache: true);
			if (!(controllerSensitivitySetter == null))
			{
				return controllerSensitivitySetter.CurrentSensitivity;
			}
		}
		return 2f;
	}

	public override void Set(float uiValue)
	{
		if (Application.isPlaying)
		{
			bool allowCache = !_resolveEverySet;
			ControllerSensitivitySetter controllerSensitivitySetter = ResolveSetter(allowCache);
			if (controllerSensitivitySetter != null)
			{
				controllerSensitivitySetter.ChangeSensitivity(uiValue);
				base.NotifyListenersIfChanged(uiValue);
			}
		}
	}

	private ControllerSensitivitySetter ResolveSetter(bool allowCache)
	{
		if (allowCache && _cachedSetter != null)
		{
			return _cachedSetter;
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
						_cachedSetter = (ControllerSensitivitySetter)obj;
						return (ControllerSensitivitySetter)obj;
					}
					if (!_logWarnings)
					{
						goto IL_023b;
					}
					string[] array = new string[5];
					if (array != null)
					{
						array[0] = "[ControllerSensitivityConnection] GameObject '";
						string name = gameObject.name;
						array[1] = name;
						array[2] = "' (tag '";
						array[3] = _targetTag;
						array[4] = "') has no ControllerSensitivitySetter component.";
						text = string.Concat(array);
						goto IL_01bf;
					}
				}
				return (ControllerSensitivitySetter)(object)new NullReferenceException();
			}
			if (_logWarnings)
			{
				text = "[ControllerSensitivityConnection] No GameObject found with tag '" + _targetTag + "'.";
				goto IL_01bf;
			}
		}
		else if (_logWarnings)
		{
			message = "[ControllerSensitivityConnection] TargetTag is empty. Cannot resolve target.";
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

	private float DefaultValue()
	{
		return 2f;
	}
}
