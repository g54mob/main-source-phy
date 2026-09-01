using UnityEngine;

public class AddPercentageToSlider : ClickBehaviour
{
	public SimpleMenuSlider simpleMenuSlider;

	public float defaultIncrease;

	private bool changeValueIn;

	private float timeDown;

	private int state;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		changeValueIn = true;
		timeDown = 0f;
		state = 0;
	}

	public override void OnClickReleased()
	{
		changeValueIn = false;
	}

	private void Update()
	{
		if (!changeValueIn)
		{
			return;
		}
		if (timeDown < 0.5f)
		{
			if (state == 0)
			{
				state = 1;
				simpleMenuSlider.Nudge(defaultIncrease);
			}
		}
		else if (state == 1)
		{
			if (timeDown < 1.5f)
			{
				simpleMenuSlider.AddPercentage(defaultIncrease * Time.deltaTime);
			}
			else
			{
				state = 2;
			}
		}
		else if (state == 2)
		{
			if (timeDown < 4f)
			{
				simpleMenuSlider.AddPercentage(defaultIncrease * Time.deltaTime * 5f);
			}
			else
			{
				state = 3;
			}
		}
		else if (state == 3)
		{
			simpleMenuSlider.AddPercentage(defaultIncrease * Time.deltaTime * 25f);
		}
		timeDown += Time.deltaTime;
	}
}
