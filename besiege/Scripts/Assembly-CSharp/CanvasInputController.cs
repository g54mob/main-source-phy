public class CanvasInputController : ICanvasInputController
{
	protected ICanvasInputView view;

	public virtual void HandleInput(string inputText)
	{
		view.AddTextEntry(inputText);
	}

	public virtual void Initialize(ICanvasInputView view)
	{
		this.view = view;
	}

	public virtual void Dispose()
	{
	}

	public virtual void OnUpdate()
	{
	}

	public virtual void OnVisibilityChanged(bool visible)
	{
	}
}
