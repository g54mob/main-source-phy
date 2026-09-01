using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Kamgam.SettingsGenerator;

public class InputBindingConnection : Connection<string>, IConnectionWithProviderAccess
{
	public static List<InputBindingConnection> Connections;

	public static bool LogErrorOnBindingFail;

	protected InputActionAsset _inputActionAsset;

	protected string _bindingId;

	private static List<InputActionAsset> _tmpAssets;

	protected SettingsProvider _provider;

	public InputBindingConnection()
	{
		Connections.Add(this);
	}

	public void SetBindingId(string id)
	{
		_bindingId = id;
	}

	public string GetBindingId()
	{
		return _bindingId;
	}

	public void SetInputActionAsset(InputActionAsset asset)
	{
		_inputActionAsset = asset;
	}

	public InputActionAsset GetInputActionAsset()
	{
		return _inputActionAsset;
	}

	public void ClearOverride()
	{
		if (!(_inputActionAsset != null))
		{
			return;
		}
		int bindingIndexWithinAction = InputActionRebindingExtensionsExtensions.GetBindingIndexWithinAction(_inputActionAsset, _bindingId);
		if (bindingIndexWithinAction >= 0)
		{
			InputAction actionOfBinding = InputActionRebindingExtensionsExtensions.GetActionOfBinding(_inputActionAsset, _bindingId);
			if (actionOfBinding != null)
			{
				InputActionRebindingExtensions.RemoveBindingOverride(actionOfBinding, bindingIndexWithinAction);
			}
		}
	}

	public override string Get()
	{
		return getBindingPath(getDefault: false);
	}

	public override string GetDefault()
	{
		return getBindingPath(getDefault: true);
	}

	protected unsafe string getBindingPath(bool getDefault)
	{
		//IL_00b3: Expected O, but got Ref
		if (_inputActionAsset != null)
		{
			if (InputActionRebindingExtensionsExtensions.FindBinding(_inputActionAsset, _bindingId, out var binding))
			{
				if (!binding.isComposite)
				{
					if (getDefault)
					{
						return null;
					}
					return binding.effectivePath;
				}
				object obj = default(object);
				return getPathsFromComposite((InputBinding)(&obj), getDefault);
			}
			logNoBindingError();
		}
		else
		{
			logNoInputAssetError();
		}
		return null;
	}

	public bool IsComposite()
	{
		if (_inputActionAsset != null)
		{
			if (!InputActionRebindingExtensionsExtensions.FindBinding(_inputActionAsset, _bindingId, out var binding))
			{
				logNoBindingError();
				return false;
			}
			return binding.isComposite;
		}
		logNoInputAssetError();
		return false;
	}

	protected unsafe string getPathsFromComposite(InputBinding binding, bool getDefault)
	{
		//IL_000e: Expected O, but got Ref
		//IL_00fd: Expected I, but got O
		Guid id = ((InputBinding*)binding)->id;
		Guid guid = default(Guid);
		string bindingId = guid.ToString();
		InputAction actionOfBinding = InputActionRebindingExtensionsExtensions.GetActionOfBinding(_inputActionAsset, bindingId);
		object obj = default(object);
		ReadOnlyArray<InputBinding> bindings = ((InputAction)(&obj)).bindings;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA390");
		string text = "";
		ReadOnlyArray<InputBinding>.Enumerator enumerator = default(ReadOnlyArray<InputBinding>.Enumerator);
		InputBinding inputBinding = default(InputBinding);
		string text3 = default(string);
		string text5 = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084D310");
				if (!inputBinding.isPartOfComposite)
				{
					continue;
				}
				string text2;
				if (getDefault)
				{
					text2 = text3;
				}
				else
				{
					string effectivePath = inputBinding.effectivePath;
					text2 = effectivePath;
				}
				if (!string.IsNullOrEmpty(text))
				{
					if (text2 == null)
					{
						break;
					}
					if (!text2.Contains("anyKey"))
					{
						nint num = (nint)typeof(InputBindingForInputSystem);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
						string text4 = text + text5 + text2;
						text = text4;
					}
				}
				else
				{
					text = text2;
				}
				continue;
			}
			enumerator.Dispose();
			return text;
		}
		throw new NullReferenceException();
	}

	private static void logNoInputAssetError()
	{
		if (LogErrorOnBindingFail)
		{
			Debug.LogError("The InputActionAsset is NULL.");
		}
	}

	private void logNoBindingError()
	{
		if (LogErrorOnBindingFail)
		{
			string message = "No binding for ID '" + _bindingId + "' found.";
			Debug.LogError(message);
		}
	}

	public override void Set(string overridePath)
	{
		SettingsProvider provider = _provider;
		if ((object)_provider != null)
		{
			UnityEngine.Object obj = ((!(provider.InputActionAsset != null)) ? _inputActionAsset : provider.InputActionAsset);
			bool flag = obj == null;
			if (!flag)
			{
				string text;
				if (provider.DontApplyBindingOverridesToAllInstances == flag && obj != null)
				{
					List<InputActionAsset> list = InputActionAssetUtils.FindInstancesOf((InputActionAsset)obj, _tmpAssets, 0.2f);
					if (list == null)
					{
						goto IL_0166;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					text = overridePath;
					List<InputActionAsset>.Enumerator enumerator = default(List<InputActionAsset>.Enumerator);
					InputActionAsset inputActionAsset = default(InputActionAsset);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						string text2 = applyOverridesToActionAsset(inputActionAsset, text);
						text = text2;
					}
					enumerator.Dispose();
				}
				else
				{
					string text3 = applyOverridesToActionAsset(_inputActionAsset, overridePath);
					text = text3;
				}
				base.NotifyListenersIfChanged(text);
			}
			else if (LogErrorOnBindingFail)
			{
				Debug.LogError("No InputActionAsset found.");
			}
			return;
		}
		goto IL_0166;
		IL_0166:
		throw new NullReferenceException();
	}

	private unsafe string applyOverridesToActionAsset(InputActionAsset inputActionAsset, string overridePath)
	{
		//IL_00be: Expected O, but got Ref
		bool flag = InputActionRebindingExtensionsExtensions.FindBinding(inputActionAsset, _bindingId, out var binding);
		bool flag2 = !flag;
		string text = overridePath;
		if (!flag2)
		{
			if (!binding.isComposite)
			{
				if (overridePath == null)
				{
					return (string)(object)new NullReferenceException();
				}
				int num = overridePath.IndexOf(InputBindingForInputSystem.CompositeControlSeparator);
				bool flag3 = num < 0;
				text = overridePath;
				if (!flag3)
				{
					string text2 = overridePath.Substring(0, num);
					text = text2;
				}
				string overrideProcessors = default(string);
				if (!InputActionRebindingExtensionsExtensions.ApplyBindingOverrideWithResult(inputActionAsset, _bindingId, text, null, overrideProcessors) && LogErrorOnBindingFail)
				{
					string message = "No binding for ID '" + _bindingId + "' found.";
					Debug.LogError(message);
				}
			}
			else
			{
				object obj = default(object);
				setPathsOnComposite((InputBinding)(&obj), overridePath);
				text = overridePath;
			}
		}
		return text;
	}

	protected unsafe void setPathsOnComposite(InputBinding binding, string compositePath)
	{
		//IL_005c: Expected O, but got Ref
		//IL_007a: Expected O, but got I4
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		string[] array = compositePath.Split(InputBindingForInputSystem.CompositeControlSeparator);
		Guid id = ((InputBinding*)binding)->id;
		Guid guid = default(Guid);
		string text = guid.ToString();
		InputAction actionOfBinding = InputActionRebindingExtensionsExtensions.GetActionOfBinding(_inputActionAsset, text);
		object obj = default(object);
		ReadOnlyArray<InputBinding> bindings = ((InputAction)(&obj)).bindings;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA390");
		object obj2 = 0;
		ReadOnlyArray<InputBinding>.Enumerator enumerator = default(ReadOnlyArray<InputBinding>.Enumerator);
		InputBinding inputBinding = default(InputBinding);
		string overrideProcessors = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084D310");
				if (!inputBinding.isPartOfComposite)
				{
					continue;
				}
				Guid id2 = inputBinding.id;
				string bindingId = guid.ToString();
				if (array == null)
				{
					break;
				}
				if (array.Length <= (nint)obj2)
				{
					if (!InputActionRebindingExtensionsExtensions.ApplyBindingOverrideWithResult(_inputActionAsset, bindingId, "<Keyboard>/anyKey", null, overrideProcessors) && LogErrorOnBindingFail)
					{
						string message = "No binding for ID '" + text + "' found.";
						Debug.LogError(message);
						obj2++;
						continue;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(array[obj2]))
					{
						string message2 = "Empty path for binding ID '" + text + "'. Skipping.";
						Debug.LogWarning(message2);
						obj2++;
						continue;
					}
					if (!InputActionRebindingExtensionsExtensions.ApplyBindingOverrideWithResult(_inputActionAsset, bindingId, array[obj2], null, overrideProcessors) && LogErrorOnBindingFail)
					{
						string message3 = "No binding for ID '" + text + "' found.";
						Debug.LogError(message3);
					}
				}
				obj2++;
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void SetProvider(SettingsProvider provider)
	{
		_provider = provider;
	}

	public SettingsProvider GetProvider()
	{
		return _provider;
	}

	static InputBindingConnection()
	{
		List<InputBindingConnection> connections = new List<InputBindingConnection>();
		Connections = connections;
		LogErrorOnBindingFail = true;
		List<InputActionAsset> tmpAssets = new List<InputActionAsset>();
		_tmpAssets = tmpAssets;
	}
}
