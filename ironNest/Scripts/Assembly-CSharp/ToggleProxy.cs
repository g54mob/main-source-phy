using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ToggleProxy : MonoBehaviour
{
	[Tooltip("Unique key used by AnimatedObjectToggle remote key lists.")]
	public string key;

	[Header("Target Resolution")]
	[Tooltip("Explicit target to be toggled. If null and autoFindChildName is set, will search.")]
	public GameObject target;

	[Tooltip("If set and 'target' is null, will search hierarchy for a child with this exact name at OnEnable.")]
	public string autoFindChildName;

	[Tooltip("If true, deactivate target in OnEnable (only if target assigned or found).")]
	public bool deactivateTargetOnEnable;

	[Header("Optional Behavior")]
	[Tooltip("Invert the meaning: when controller requests 'true', we set target inactive (and vice versa).")]
	public bool invert;

	[Tooltip("If true, only respond to 'true' transitions (ignore 'false'). Useful for one-shot activation. (No auto revert).")]
	public bool oneShotActivate;

	[Tooltip("If true, only respond to 'false' transitions (ignore 'true').")]
	public bool oneShotDeactivate;

	private static readonly Dictionary<string, HashSet<ToggleProxy>> _registry;

	public static event Action<ToggleProxy> OnProxyRegistered
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void ResolveTargetIfNeeded()
	{
	}

	internal static void ApplyToKey(string key, bool state)
	{
	}

	public void ApplyActive(bool requestedState)
	{
	}
}
