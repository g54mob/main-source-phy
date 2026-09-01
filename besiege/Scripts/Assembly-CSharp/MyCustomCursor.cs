using UnityEngine;

public class MyCustomCursor : MonoBehaviour
{
	public Texture2D yourCursor;

	public int cursorSizeX = 16;

	public int cursorSizeY = 16;

	private void Start()
	{
		Cursor.visible = false;
	}

	private void OnGUI()
	{
		GUI.DrawTexture(new Rect(Event.current.mousePosition.x - (float)(cursorSizeX / 2), Event.current.mousePosition.y - (float)(cursorSizeY / 2), cursorSizeX, cursorSizeY), yourCursor);
	}
}
