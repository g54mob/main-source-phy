using UnityEngine;

public class AngleWheelController : ClickBehaviour
{
	public Renderer angleRender;

	public float myValue = 0.5f;

	public int angleToBe = 45;

	public float sensitivity = 4f;

	public bool flip;

	public TextMesh degreesTextMesh;

	public bool negative;

	public override void OnClickDrag()
	{
		if (flip)
		{
			myValue -= InputManager.MouseY() * Mathf.Abs(InputManager.MouseY()) * sensitivity;
		}
		else
		{
			myValue += InputManager.MouseY() * Mathf.Abs(InputManager.MouseY()) * sensitivity;
		}
	}

	private void Update()
	{
		angleRender.material.mainTextureOffset = new Vector2(myValue, 0f);
		if (flip)
		{
			myValue = Mathf.Clamp(myValue, -1f, 0f);
		}
		else
		{
			myValue = Mathf.Clamp(myValue, 0f, 1f);
		}
		if (negative)
		{
			degreesTextMesh.text = "-" + (365f + (float)Mathf.RoundToInt(myValue * 365f));
		}
		else
		{
			degreesTextMesh.text = string.Empty + (365f - (float)Mathf.RoundToInt(myValue * -1f * 365f));
		}
	}
}
