using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ImpactCorrectionTierController : MonoBehaviour
{
	[Header("Tier Roots")]
	[Tooltip("Parent transform containing all Distance Tier GameObjects (each with a CorrectionDistanceTierConfig).")]
	public Transform distanceTierRoot;

	[Tooltip("Parent transform containing all Direction Tier GameObjects (each with a CorrectionDirectionTierConfig).")]
	public Transform directionTierRoot;

	[Header("Auto Reevaluation")]
	[Tooltip("If true, the controller will automatically reevaluate active tiers whenever any tier config enables or disables.")]
	public bool autoReevaluateOnTierStateChange;

	private static bool _queuedGlobalReevaluate;

	private readonly List<CorrectionDistanceTierConfig> _distanceTiers;

	private readonly List<CorrectionDirectionTierConfig> _directionTiers;

	public CorrectionDistanceTierConfig ActiveDistanceTier { get; private set; }

	public CorrectionDirectionTierConfig ActiveDirectionTier { get; private set; }

	public static ImpactCorrectionTierController Instance { get; private set; }

	public static event Action OnActiveTiersChanged
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

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	public void ReevaluateNow()
	{
	}

	private void CacheTiers()
	{
	}

	private void EvaluateActiveTiers(bool invokeEvent = true)
	{
	}

	private T GetHighestActive<T>(List<T> list) where T : MonoBehaviour
	{
		return null;
	}

	private void ApplyPointerVisualSelection()
	{
	}

	internal void HandleTierStateChanged()
	{
	}

	public static void ScheduleGlobalReevaluate()
	{
	}
}
