using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ControllerButtonImage : MonoBehaviour
{
	private Image image;

	[FormerlySerializedAs("state")]
	public ControllerButtonActionType controllerButton;

	public ControllerButtonActionType controllerButtonAlt;

	public bool useAxis;

	public ControllerAxisActionType stateAxis;

	private bool forceTryAgain;

	public bool useDarkIcon;

	private bool useDarkIconLastFrame;

	public static Color darkIconColor => new Color(0.34901962f, 0.34901962f, 0.34901962f, 1f);

	private void OnEnable()
	{
		Controller.init();
		sync();
	}

	public void sync()
	{
		if (image == null)
		{
			image = GetComponent<Image>();
		}
		Sprite sprite = null;
		if (useAxis)
		{
			sprite = Controller.getGlyph(stateAxis);
		}
		else
		{
			sprite = Controller.getGlyph(controllerButton);
			if (sprite == null)
			{
				sprite = Controller.getGlyph(controllerButtonAlt);
			}
		}
		if (sprite != null)
		{
			image.sprite = sprite;
			image.type = Image.Type.Simple;
			image.preserveAspect = true;
			forceTryAgain = false;
		}
		else
		{
			forceTryAgain = true;
		}
		if (useDarkIcon)
		{
			image.color = darkIconColor;
		}
		else if (GetComponent<Shadow>() == null)
		{
			base.gameObject.AddComponent<Shadow>().effectDistance = new Vector2(2f, -2f);
		}
	}

	public void Update()
	{
		if (Controller.controllerModeChanged || Controller.controllerInputSetChanged || image.sprite == null || forceTryAgain || useDarkIcon != useDarkIconLastFrame)
		{
			sync();
		}
	}
}
