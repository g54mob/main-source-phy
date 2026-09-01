using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class AnimatedObjectToggle : MonoBehaviour
{
	[Header("Animate This Bool (Add in Animator)")]
	public bool ToggleOn;

	[Header("Local Targets (Direct References)")]
	public List<GameObject> activateWhenTrue;

	public List<GameObject> deactivateWhenTrue;

	[Header("Remote Targets (Keys)")]
	[Tooltip("Keys for proxies whose targets are active while ToggleOn == true.")]
	public List<string> remoteActivateKeys;

	[Tooltip("Keys for proxies whose targets are active while ToggleOn == false.")]
	public List<string> remoteDeactivateKeys;

	[Header("Behavior")]
	[Tooltip("Apply current ToggleOn value immediately when enabled.")]
	public bool applyOnEnable;

	[Tooltip("Evaluate after Animator (LateUpdate) instead of Update.")]
	public bool evaluateInLateUpdate;

	[Header("Events")]
	public UnityEvent onBecameTrue;

	public UnityEvent onBecameFalse;

	private bool _lastState;

	private static readonly HashSet<AnimatedObjectToggle> _controllers;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
	}

	private void ApplyIfChanged(bool force)
	{
	}

	private void SetListActive(List<GameObject> list, bool state)
	{
	}

	private void SetRemoteKeys(List<string> keys, bool state)
	{
	}

	private void HandleProxyRegistered(ToggleProxy proxy)
	{
	}

	[ContextMenu("Set True (Test)")]
	private void ContextSetTrue()
	{
	}

	[ContextMenu("Set False (Test)")]
	private void ContextSetFalse()
	{
	}

	[ContextMenu("Force Refresh")]
	private void ContextForceRefresh()
	{
	}

	public static void ForceRefreshAll()
	{
	}
}
