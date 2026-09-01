using UnityEngine;
using UnityEngine.Events;

public class SystemDependantClick : MonoBehaviour
{
	[SerializeField]
	private UnityEvent keyboardClick;

	[SerializeField]
	private UnityEvent gamepadClick;

	[SerializeField]
	private DynamicCursorManager cursorManager;

	public void SystemDependantClickCheck()
	{
	}
}
