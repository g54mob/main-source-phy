using System;
using UnityEngine;

namespace TrajectorySystem;

public static class TrajectoryTargetRegistry
{
	public static TrajectoryTarget TryClaimAnyWithTag(string targetTag, UnityEngine.Object owner)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		if (!string.IsNullOrWhiteSpace(targetTag))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(targetTag);
			if (array == null)
			{
				goto IL_0150;
			}
			object obj = array + 32;
			TrajectoryTarget trajectoryTarget = null;
			TrajectoryTarget component = null;
			for (TrajectoryTarget trajectoryTarget2 = null; (nint)trajectoryTarget2 < array.Length; trajectoryTarget = (TrajectoryTarget)(trajectoryTarget + 1), obj += 8, trajectoryTarget2 = trajectoryTarget)
			{
				if (!((UnityEngine.Object)obj != null))
				{
					continue;
				}
				if (obj != null)
				{
					if (!((GameObject)obj).TryGetComponent(out component))
					{
						continue;
					}
					if ((object)component != null)
					{
						if (!component.TryClaim(owner))
						{
							continue;
						}
						return component;
					}
				}
				goto IL_0150;
			}
		}
		return null;
		IL_0150:
		return (TrajectoryTarget)(object)new NullReferenceException();
	}
}
