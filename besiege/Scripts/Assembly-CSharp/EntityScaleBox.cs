public class EntityScaleBox : ClickBehaviour
{
	public enum MouseState
	{
		Enter = 0,
		Exit = 1,
		Drag = 2,
		Down = 3,
		Up = 4
	}

	public EntityScaleTool scaleTool;

	protected void OnMouseEnter()
	{
		scaleTool.ToolStateUpdate(MouseState.Enter);
	}

	protected void OnMouseExit()
	{
		scaleTool.ToolStateUpdate(MouseState.Exit);
	}

	public override void OnClicked()
	{
		scaleTool.ToolStateUpdate(MouseState.Down);
	}

	public override void OnClickDrag()
	{
		scaleTool.ToolStateUpdate(MouseState.Drag);
	}

	public override void OnClickReleased()
	{
		scaleTool.ToolStateUpdate(MouseState.Up);
	}
}
