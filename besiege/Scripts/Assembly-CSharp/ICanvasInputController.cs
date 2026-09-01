public interface ICanvasInputController
{
	void Initialize(ICanvasInputView view);

	void HandleInput(string inputText);

	void Dispose();

	void OnUpdate();

	void OnVisibilityChanged(bool visible);
}
