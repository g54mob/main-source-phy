using UnityEngine;
using UnityEngine.UI;

public class VRTutorialControllerButton : MonoBehaviour
{
	public enum ButtonType
	{
		Trigger = 0,
		Grip = 1,
		PrimaryButton = 2,
		SecondaryButton = 3,
		Thumbstick = 4,
		Menu = 5
	}

	public LineRenderer line;

	public Transform[] lines;

	public Text text;

	public ButtonType type;
}
