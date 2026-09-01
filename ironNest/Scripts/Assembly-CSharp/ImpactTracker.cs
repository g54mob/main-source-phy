using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ImpactTracker : MonoBehaviour
{
	public static Dictionary<string, EntityLocation> EntityLocations;

	public static event Action<Vector2, float> OnImpact
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

	public static void RegisterEntity(EntityLocation entity)
	{
	}

	public static void UnregisterEntity(string entityID)
	{
	}

	public static void EvaluateImpact(ShellDefinition shell, Vector2 impactLocation, bool triggerNormalEvents = true)
	{
	}

	public static (EntityLocation, float, float) GetNearestTargetOrEnemy(Vector2 fromPosition)
	{
		return default((EntityLocation, float, float));
	}

	public static (EntityLocation, float, float) GetNearest(EntityRoles role, Vector2 fromPosition, bool isAlive = true)
	{
		return default((EntityLocation, float, float));
	}
}
