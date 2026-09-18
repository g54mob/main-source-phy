using UnityEngine;

public class TweenDataStore : MonoBehaviour
{
	public static float GetTweenDuration(TweenDuration a)
	{
		return a switch
		{
			TweenDuration.Very_Instant => 0.03f, 
			TweenDuration.Instant => 0.1f, 
			TweenDuration.Super_Short => 0.25f, 
			TweenDuration.Very_Short => 0.5f, 
			TweenDuration.Short => 1f, 
			TweenDuration.Medium => 2f, 
			TweenDuration.Long => 3f, 
			TweenDuration.Very_Long => 6f, 
			_ => 0.5f, 
		};
	}

	public static TweenDuration GetFasterTweenDuration(TweenDuration a)
	{
		return a switch
		{
			TweenDuration.Instant => TweenDuration.Very_Instant, 
			TweenDuration.Super_Short => TweenDuration.Instant, 
			TweenDuration.Very_Short => TweenDuration.Super_Short, 
			TweenDuration.Short => TweenDuration.Very_Short, 
			TweenDuration.Medium => TweenDuration.Short, 
			TweenDuration.Long => TweenDuration.Medium, 
			TweenDuration.Very_Long => TweenDuration.Long, 
			_ => TweenDuration.Very_Instant, 
		};
	}

	public static float GetFasterTweenDurationValue(TweenDuration a)
	{
		return a switch
		{
			TweenDuration.Very_Instant => GetTweenDuration(TweenDuration.Very_Instant), 
			TweenDuration.Instant => GetTweenDuration(TweenDuration.Very_Instant), 
			TweenDuration.Super_Short => GetTweenDuration(TweenDuration.Instant), 
			TweenDuration.Very_Short => GetTweenDuration(TweenDuration.Super_Short), 
			TweenDuration.Short => GetTweenDuration(TweenDuration.Very_Short), 
			TweenDuration.Medium => GetTweenDuration(TweenDuration.Short), 
			TweenDuration.Long => GetTweenDuration(TweenDuration.Medium), 
			TweenDuration.Very_Long => GetTweenDuration(TweenDuration.Long), 
			_ => GetTweenDuration(TweenDuration.Very_Instant), 
		};
	}

	public static TweenDuration GetSlowerTweenDuration(TweenDuration a)
	{
		return a switch
		{
			TweenDuration.Very_Instant => TweenDuration.Instant, 
			TweenDuration.Instant => TweenDuration.Super_Short, 
			TweenDuration.Super_Short => TweenDuration.Very_Short, 
			TweenDuration.Very_Short => TweenDuration.Short, 
			TweenDuration.Short => TweenDuration.Medium, 
			TweenDuration.Medium => TweenDuration.Long, 
			TweenDuration.Long => TweenDuration.Very_Long, 
			TweenDuration.Very_Long => TweenDuration.Very_Instant, 
			_ => TweenDuration.Very_Long, 
		};
	}

	public static float GetSlowerTweenDurationValue(TweenDuration a)
	{
		return a switch
		{
			TweenDuration.Very_Instant => GetTweenDuration(TweenDuration.Instant), 
			TweenDuration.Instant => GetTweenDuration(TweenDuration.Super_Short), 
			TweenDuration.Super_Short => GetTweenDuration(TweenDuration.Very_Short), 
			TweenDuration.Very_Short => GetTweenDuration(TweenDuration.Short), 
			TweenDuration.Short => GetTweenDuration(TweenDuration.Medium), 
			TweenDuration.Medium => GetTweenDuration(TweenDuration.Long), 
			TweenDuration.Long => GetTweenDuration(TweenDuration.Very_Long), 
			TweenDuration.Very_Long => GetTweenDuration(TweenDuration.Very_Instant), 
			_ => GetTweenDuration(TweenDuration.Very_Long), 
		};
	}

	public static float GetShakeStrength()
	{
		return 10f;
	}

	public static int GetShakeVibration()
	{
		return 25;
	}
}
