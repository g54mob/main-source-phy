using UnityEngine;

public class UnitEditorState : MonoBehaviour
{
	public enum EditorState
	{
		Base = 0,
		PlacingObject = 1,
		ColorPicking = 2
	}

	public static EditorState editorState;

	public static void SetState(EditorState newState)
	{
		editorState = newState;
	}
}
