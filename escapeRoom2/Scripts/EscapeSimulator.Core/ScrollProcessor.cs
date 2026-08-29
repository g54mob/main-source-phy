using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollProcessor : InputProcessor<Vector2>
{
	public float scrollMultiplier = 20f;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void registerProcessor()
	{
		InputSystem.RegisterProcessor<ScrollProcessor>();
	}

	public override Vector2 Process(Vector2 value, InputControl control)
	{
		return value * scrollMultiplier;
	}
}
