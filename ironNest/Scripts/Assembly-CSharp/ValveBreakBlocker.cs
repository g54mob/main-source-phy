using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ValveBreakBlocker : MonoBehaviour
{
	[Header("Global Blocking")]
	[Tooltip("If true, this component instance will globally block ALL valve breaking while enabled.\nUse for tutorials, cutscenes, or mission setup when you need a complete stop. Safe defaults:\n- Enable during the phase to enforce the stop; disable when the phase ends.\nMultiple instances can be enabled; global blocked = true if ANY instance enables this.\nDesign Note: This affects ALL systems, regardless of per-system settings.")]
	[SerializeField]
	private bool blockAllSystems;

	[Header("Per-System Blocking")]
	[Tooltip("If true, this component instance will block valve breaking for ONLY the listed System IDs while enabled.\nMatch is exact and case-sensitive. IDs must match HighPressureSystemManager.SystemId.\nExamples: \"Default\", \"ReactorA\", \"UpperDeck\".\nSafe usage: Enable this to block specific systems without affecting others. You can have multiple instances,\neach blocking different sets. Disabling the instance will remove its per-system blocks.")]
	[SerializeField]
	private bool blockSpecificSystems;

	[Tooltip("System IDs to block when 'Block Specific Systems' is enabled. Exact, case-sensitive matches required.\nRules: Non-empty strings; avoid whitespace. Examples: \"Default\", \"ReactorA\".\nDesigner Tip: Keep IDs short and consistent per system. Coordinate with HighPressureSystemManager.SystemId.")]
	[SerializeField]
	private List<string> blockedSystemIds;

	[Header("Debug Logging")]
	[Tooltip("If true, logs state changes (global and per-system) to the Console. Useful for mission setup and debugging.")]
	[SerializeField]
	private bool logDebug;

	private static int s_globalBlockerCount;

	private static readonly HashSet<string> s_blockedSystems;

	public static bool IsBlocked => false;

	public static IReadOnlyCollection<string> BlockedSystems => null;

	public static event Action<bool> OnBlockStateChanged
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

	public static event Action<string[]> OnPerSystemBlocksChanged
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

	public static bool IsSystemBlocked(string systemId)
	{
		return false;
	}

	public static void AddSystemBlock(string systemId)
	{
	}

	public static void RemoveSystemBlock(string systemId)
	{
	}

	public static void ClearSystemBlocks()
	{
	}

	private static void NotifyGlobalChanged()
	{
	}

	private static void NotifyPerSystemBlocksChanged()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnValidate()
	{
	}
}
