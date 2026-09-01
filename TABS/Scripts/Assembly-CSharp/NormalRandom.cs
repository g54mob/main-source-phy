using UnityEngine;

[CreateAssetMenu(fileName = "BellCurve", menuName = "Landfall/Random/BellCurve")]
public class NormalRandom : ScriptableObject
{
	public AnimationCurve BellCurve;

	public AnimationCurve SuperBellCurve;

	private static NormalRandom instance;

	public static NormalRandom Instance
	{
		get
		{
			if (!instance)
			{
				instance = Resources.Load("BellCurve") as NormalRandom;
			}
			return instance;
		}
	}

	public static float GetRandom(float min, float max)
	{
		float t = Instance.BellCurve.Evaluate(Random.value);
		return Mathf.Lerp(min, max, t);
	}

	public static float GetSuperRandom(float min, float max)
	{
		float t = Instance.SuperBellCurve.Evaluate(Random.value);
		return Mathf.Lerp(min, max, t);
	}

	public static float GetSuperRandomSquared(float min, float max)
	{
		float t = Instance.SuperBellCurve.Evaluate((Random.value + Random.value) / 2f);
		return Mathf.Lerp(min, max, t);
	}
}
