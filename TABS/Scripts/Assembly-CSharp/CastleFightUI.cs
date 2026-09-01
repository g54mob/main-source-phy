using UnityEngine;

public class CastleFightUI : MonoBehaviour
{
	public static CastleFightUI instance;

	public CodeAnimation[] blue;

	public CodeAnimation[] red;

	private void Awake()
	{
		instance = this;
	}

	public void Score(int redScores, int BlueScores)
	{
		for (int i = 0; i < redScores; i++)
		{
			if (red[i].currentState != CodeAnimationInstance.AnimationUse.In)
			{
				red[i].PlayIn();
			}
		}
		for (int j = 0; j < BlueScores; j++)
		{
			if (red[j].currentState != CodeAnimationInstance.AnimationUse.In)
			{
				blue[j].PlayIn();
			}
		}
	}
}
