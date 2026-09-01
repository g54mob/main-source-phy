using UnityEngine;

public class CastleFightMenuHandler : MonoBehaviour
{
	public enum InputType
	{
		Left = 0,
		Right = 1,
		Click = 2
	}

	[HideInInspector]
	public CastleFightPlacer placer;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Navigate(int inputDirection, bool click)
	{
	}

	public void Transition()
	{
	}
}
