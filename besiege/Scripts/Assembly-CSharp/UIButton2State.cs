using System;
using UnityEngine;

[AddComponentMenu("UI/UI Button (2 State)")]
public class UIButton2State : UIButton
{
	[Serializable]
	public class State
	{
		public Renderer BG;

		public GameObject Icon;

		[HideInInspector]
		public Color scolor = Color.black;

		[HideInInspector]
		public Color ecolor = Color.black;
	}

	public State state1 = new State();

	public State state2 = new State();

	public MonoBehaviour[] scripts;

	private int state = 1;

	public void SetToState(int s)
	{
		if (state2.scolor == Color.black)
		{
			state2.scolor = state2.BG.material.GetColor("_TintColor");
			float num = (state2.scolor.r + state2.scolor.g + state2.scolor.b) / 3f;
			state2.ecolor = new Color(num, num, num, state2.scolor.a);
		}
		if (s == 1)
		{
			state = 1;
			state1.BG.gameObject.SetActive(true);
			state1.Icon.SetActive(true);
			state2.BG.gameObject.SetActive(false);
			state2.Icon.SetActive(false);
			for (int i = 0; i < scripts.Length; i++)
			{
				scripts[i].enabled = true;
			}
		}
		else
		{
			state = 2;
			state1.BG.gameObject.SetActive(false);
			state1.Icon.SetActive(false);
			state2.BG.gameObject.SetActive(true);
			state2.Icon.SetActive(true);
			for (int j = 0; j < scripts.Length; j++)
			{
				scripts[j].enabled = false;
			}
		}
	}

	public void Toggle(bool? enabled = null)
	{
		if (!enabled.HasValue)
		{
			return;
		}
		switch (state)
		{
		case 1:
			state1.BG.gameObject.SetActive(enabled.Value);
			break;
		case 2:
			if (enabled.HasValue)
			{
				switch (enabled.Value)
				{
				case true:
					state2.BG.material.SetColor("_TintColor", state2.scolor);
					break;
				case false:
					state2.BG.material.SetColor("_TintColor", state2.ecolor);
					break;
				}
			}
			break;
		}
	}
}
