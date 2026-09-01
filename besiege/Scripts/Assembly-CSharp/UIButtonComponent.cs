using UnityEngine;

[RequireComponent(typeof(SimpleUIButton))]
[AddComponentMenu("UI/UI Button Component")]
public abstract class UIButtonComponent : MonoBehaviour
{
	private SimpleUIButton simpleUIButton;

	protected virtual void Awake()
	{
		simpleUIButton = GetComponent<SimpleUIButton>();
		simpleUIButton.MouseEnter += OnButtonMouseEnter;
		simpleUIButton.MouseExit += OnButtonMouseExit;
		simpleUIButton.Click += OnButtonClicked;
	}

	protected abstract void OnButtonMouseExit();

	protected abstract void OnButtonMouseEnter();

	protected abstract void OnButtonClicked();
}
