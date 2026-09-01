using UnityEngine;

public class CustomResolutionController : ClickBehaviour
{
	public int myResX = 640;

	public int myResY = 480;

	public bool xRes = true;

	public TextMesh myTextMesh;

	public float sensitivity = 1f;

	private float floatResX;

	private float floatResY;

	private void OnEnable()
	{
		if (xRes)
		{
			myResX = OptionsMaster.BesiegeConfig.ScreenWidth;
		}
		else
		{
			myResY = OptionsMaster.BesiegeConfig.ScreenHeight;
		}
	}

	public override void OnClickHeld()
	{
		if (xRes)
		{
			floatResX += InputManager.MouseX() * sensitivity;
		}
		else
		{
			floatResY += InputManager.MouseX() * sensitivity;
		}
		myResX = Mathf.RoundToInt(floatResX);
		myResY = Mathf.RoundToInt(floatResY);
		SetTextMesh();
	}

	private void SetTextMesh()
	{
		if (xRes)
		{
			myTextMesh.text = string.Empty + myResX;
		}
		else
		{
			myTextMesh.text = string.Empty + myResY;
		}
	}
}
