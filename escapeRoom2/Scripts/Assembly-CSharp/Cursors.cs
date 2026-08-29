using UnityEngine;

public class Cursors : MonoBehaviour
{
	public Texture2D handCursor;

	public Texture2D grabCursor;

	public Texture2D clickCursor;

	public void ActivateHandCursor()
	{
		Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
	}

	public void ActivateGrabCursor()
	{
		Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
	}

	public void ActivateClickCursor()
	{
		Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
	}

	public void DeactivateCursors()
	{
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}
}
