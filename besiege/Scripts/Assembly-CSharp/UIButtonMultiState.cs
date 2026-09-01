using System;
using UnityEngine;

[AddComponentMenu("UI/UI Button (Multi State)")]
public class UIButtonMultiState : UIButton
{
	[Serializable]
	public class State
	{
		public Renderer BG;

		public GameObject Icon;

		public MonoBehaviour[] scripts;
	}

	public State[] states;

	private int state = -1;

	public void SetToState(int s)
	{
		if (this.state == s)
		{
			return;
		}
		this.state = s;
		for (int i = 0; i < states.Length; i++)
		{
			State state = states[i];
			if (state.BG != null)
			{
				state.BG.gameObject.SetActive(i == s);
			}
			if (state.Icon != null)
			{
				state.Icon.SetActive(i == s);
			}
			for (int j = 0; j < state.scripts.Length; j++)
			{
				state.scripts[j].enabled = i == s;
			}
		}
	}
}
