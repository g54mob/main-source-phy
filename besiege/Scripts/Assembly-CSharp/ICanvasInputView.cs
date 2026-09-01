public interface ICanvasInputView
{
	bool IsVisible { get; }

	void AddTextEntry(string textEntry);

	void Clear();

	void SetVisibility(bool visible);
}
