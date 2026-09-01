using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class AnimatedObjectToggle : MonoBehaviour
{
	public bool ToggleOn;

	public List<GameObject> activateWhenTrue;

	public List<GameObject> deactivateWhenTrue;

	public List<string> remoteActivateKeys;

	public List<string> remoteDeactivateKeys;

	public bool applyOnEnable;

	public bool evaluateInLateUpdate;

	public UnityEvent onBecameTrue;

	public UnityEvent onBecameFalse;

	private bool _lastState;

	private static readonly HashSet<AnimatedObjectToggle> _controllers;

	private void Awake()
	{
		bool lastState = !ToggleOn;
		_lastState = lastState;
	}

	private void OnEnable()
	{
		_controllers.Add(this);
		Action<ToggleProxy> value = HandleProxyRegistered;
		ToggleProxy.OnProxyRegistered += value;
		if (applyOnEnable)
		{
			ApplyIfChanged(force: true);
		}
	}

	private void OnDisable()
	{
		Action<ToggleProxy> value = HandleProxyRegistered;
		ToggleProxy.OnProxyRegistered -= value;
		bool flag = _controllers.Remove(this);
	}

	private void Update()
	{
		if (!evaluateInLateUpdate)
		{
			ApplyIfChanged(force: false);
		}
	}

	private void LateUpdate()
	{
		if (evaluateInLateUpdate)
		{
			ApplyIfChanged(force: false);
		}
	}

	private void ApplyIfChanged(bool force)
	{
		if (force || ToggleOn != _lastState)
		{
			_lastState = ToggleOn;
			UnityEvent unityEvent;
			if (!ToggleOn)
			{
				SetListActive(activateWhenTrue, state: false);
				SetListActive(deactivateWhenTrue, state: true);
				SetRemoteKeys(remoteActivateKeys, state: false);
				SetRemoteKeys(remoteDeactivateKeys, state: true);
				unityEvent = onBecameFalse;
			}
			else
			{
				SetListActive(activateWhenTrue, state: true);
				SetListActive(deactivateWhenTrue, state: false);
				SetRemoteKeys(remoteActivateKeys, state: true);
				SetRemoteKeys(remoteDeactivateKeys, state: false);
				unityEvent = onBecameTrue;
			}
			unityEvent?.Invoke();
		}
	}

	private void SetListActive(List<GameObject> list, bool state)
	{
		//IL_000e: Expected O, but got I4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		if (list == null)
		{
			return;
		}
		object obj = 0;
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while ((nint)obj < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((bool)obj2)
			{
				bool activeSelf = ((GameObject)obj2).activeSelf;
				if (activeSelf != state)
				{
					((GameObject)obj2).SetActive(state);
				}
			}
			obj++;
		}
	}

	private void SetRemoteKeys(List<string> keys, bool state)
	{
		//IL_000e: Expected O, but got I4
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		if (keys == null)
		{
			return;
		}
		object obj = 0;
		string text = default(string);
		while ((nint)obj < keys._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!string.IsNullOrWhiteSpace(text))
			{
				ToggleProxy.ApplyToKey(text, state);
			}
			obj++;
		}
	}

	private void HandleProxyRegistered(ToggleProxy proxy)
	{
		if (!(proxy != null) || string.IsNullOrEmpty(proxy.key))
		{
			return;
		}
		bool requestedState;
		if (!remoteActivateKeys.Contains(proxy.key))
		{
			if (!remoteDeactivateKeys.Contains(proxy.key))
			{
				return;
			}
			bool flag = !ToggleOn;
			requestedState = flag;
		}
		else
		{
			requestedState = ToggleOn;
		}
		proxy.ApplyActive(requestedState);
	}

	private void ContextSetTrue()
	{
		ToggleOn = true;
		_lastState = true;
		SetListActive(activateWhenTrue, state: true);
		SetListActive(deactivateWhenTrue, state: false);
		SetRemoteKeys(remoteActivateKeys, state: true);
		SetRemoteKeys(remoteDeactivateKeys, state: false);
		if (onBecameTrue != null)
		{
			onBecameTrue.Invoke();
		}
	}

	private void ContextSetFalse()
	{
		ToggleOn = false;
		_lastState = false;
		SetListActive(activateWhenTrue, state: false);
		SetListActive(deactivateWhenTrue, state: true);
		SetRemoteKeys(remoteActivateKeys, state: false);
		SetRemoteKeys(remoteDeactivateKeys, state: true);
		if (onBecameFalse != null)
		{
			onBecameFalse.Invoke();
		}
	}

	private void ContextForceRefresh()
	{
		ApplyIfChanged(force: true);
	}

	public static void ForceRefreshAll()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
		HashSet<AnimatedObjectToggle>.Enumerator enumerator = default(HashSet<AnimatedObjectToggle>.Enumerator);
		AnimatedObjectToggle animatedObjectToggle = default(AnimatedObjectToggle);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)animatedObjectToggle == null)
				{
					break;
				}
				animatedObjectToggle._lastState = animatedObjectToggle.ToggleOn;
				UnityEvent unityEvent;
				if (!animatedObjectToggle.ToggleOn)
				{
					animatedObjectToggle.SetListActive(animatedObjectToggle.activateWhenTrue, state: false);
					animatedObjectToggle.SetListActive(animatedObjectToggle.deactivateWhenTrue, state: true);
					animatedObjectToggle.SetRemoteKeys(animatedObjectToggle.remoteActivateKeys, state: false);
					animatedObjectToggle.SetRemoteKeys(animatedObjectToggle.remoteDeactivateKeys, state: true);
					unityEvent = animatedObjectToggle.onBecameFalse;
				}
				else
				{
					animatedObjectToggle.SetListActive(animatedObjectToggle.activateWhenTrue, state: true);
					animatedObjectToggle.SetListActive(animatedObjectToggle.deactivateWhenTrue, state: false);
					animatedObjectToggle.SetRemoteKeys(animatedObjectToggle.remoteActivateKeys, state: true);
					animatedObjectToggle.SetRemoteKeys(animatedObjectToggle.remoteDeactivateKeys, state: false);
					unityEvent = animatedObjectToggle.onBecameTrue;
				}
				unityEvent?.Invoke();
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public AnimatedObjectToggle()
	{
		List<GameObject> list = new List<GameObject>();
		activateWhenTrue = list;
		deactivateWhenTrue = new List<GameObject>();
		remoteActivateKeys = new List<string>();
		remoteDeactivateKeys = new List<string>();
		applyOnEnable = true;
		base._002Ector();
	}

	static AnimatedObjectToggle()
	{
		HashSet<AnimatedObjectToggle> controllers = new HashSet<AnimatedObjectToggle>();
		_controllers = controllers;
	}
}
