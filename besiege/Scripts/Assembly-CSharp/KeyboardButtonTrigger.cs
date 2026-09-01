using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Misc/Keyboard Button Trigger")]
public class KeyboardButtonTrigger : MonoBehaviour
{
	public KeyCode key;

	public bool onlyEditor;

	public UnityEvent OnKey;

	public void Awake()
	{
		if (onlyEditor)
		{
			Object.DestroyImmediate(this);
		}
	}

	private void Update()
	{
		if (InputManager.GetKeyDown(key))
		{
			OnKey.Invoke();
		}
	}
}
