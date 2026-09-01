using UnityEngine;

public class FPSWeaponUI : MonoBehaviour
{
	private SwingHandler swingHandler;

	public GameObject swingUI;

	public ScaleJiggle topSwing;

	public ScaleJiggle botSwing;

	public ScaleJiggle leftSwing;

	public ScaleJiggle rightSwing;

	private void Start()
	{
		swingHandler = base.transform.root.GetComponentInChildren<SwingHandler>();
	}

	private void Update()
	{
		SwingStuff();
	}

	private void SwingStuff()
	{
		if ((bool)swingUI)
		{
			topSwing.targetScale = 1f;
			botSwing.targetScale = 1f;
			rightSwing.targetScale = 1f;
			leftSwing.targetScale = 1f;
			float targetScale = 2f;
			switch (swingHandler.holdDirection)
			{
			case SwingHandler.HoldDirection.Left:
				leftSwing.targetScale = targetScale;
				break;
			case SwingHandler.HoldDirection.Up:
				topSwing.targetScale = targetScale;
				break;
			case SwingHandler.HoldDirection.Right:
				rightSwing.targetScale = targetScale;
				break;
			case SwingHandler.HoldDirection.Down:
				botSwing.targetScale = targetScale;
				break;
			case SwingHandler.HoldDirection.None:
				break;
			}
		}
	}
}
