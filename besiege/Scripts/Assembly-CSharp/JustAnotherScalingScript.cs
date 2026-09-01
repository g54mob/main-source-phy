using UnityEngine;

public class JustAnotherScalingScript : MonoBehaviour
{
	public float max = 1.75f;

	public float min = 1f;

	public float mult = 0.9f;

	protected Vector3 originalSize;

	protected float targetScale = 1f;

	protected float currentScale = 1f;

	private float setGoalNextUpdate = float.NaN;

	protected void Start()
	{
		originalSize = base.transform.localScale;
	}

	protected void Update()
	{
		if (!float.IsNaN(setGoalNextUpdate))
		{
			targetScale = setGoalNextUpdate;
			setGoalNextUpdate = float.NaN;
		}
		if (targetScale > currentScale)
		{
			currentScale = max + min - (max + min - currentScale) * mult;
			if (targetScale < currentScale)
			{
				currentScale = targetScale;
			}
		}
		else if (targetScale < currentScale)
		{
			currentScale *= mult;
			if (targetScale > currentScale)
			{
				currentScale = targetScale;
			}
		}
		base.transform.localScale = currentScale * originalSize;
	}

	public void SetGoal(float p)
	{
		setGoalNextUpdate = (max - min) * p + min;
	}

	public void SetCurrent(float p)
	{
		currentScale = (max - min) * p + min;
	}
}
