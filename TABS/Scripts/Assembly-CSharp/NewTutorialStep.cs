using System;

[Serializable]
public class NewTutorialStep
{
	public enum Tip
	{
		Movement = 0,
		UpDowm = 1,
		Look = 2,
		Possession_Enter = 4,
		Possession_Attack = 5,
		Possession_SwitchCamera = 6,
		Possession_Exit = 7
	}

	public Tip tip;
}
