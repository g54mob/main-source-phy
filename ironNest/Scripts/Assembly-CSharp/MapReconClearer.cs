using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapReconClearer : MonoBehaviour
{
	[Header("Callbacks")]
	[Tooltip("Fired after all recon photos have been destroyed. Use this to play a clear SFX, update UI state, log analytics, etc.")]
	public UnityEvent OnCleared;

	[Header("Debug")]
	[Tooltip("When enabled, logs how many impacts were cleared each time ClearAll() runs. Disable in shipping builds.")]
	[SerializeField]
	private bool _debugLog;

	private readonly List<MapReconClearHandle> _handles;

	public int ActiveCount => 0;

	public void Register(MapReconClearHandle handle)
	{
	}

	public void Unregister(MapReconClearHandle handle)
	{
	}

	public void ClearAll()
	{
	}
}
