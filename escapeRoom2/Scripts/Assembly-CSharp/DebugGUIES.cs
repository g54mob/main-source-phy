using UnityEngine;

public class DebugGUIES : MonoBehaviour
{
	public static DebugGUIES instance;

	[DebugGUIGraph(0f, 1f, 0f, -1f, 15f, 0, true)]
	public float a;

	[DebugGUIGraph(1f, 0f, 0f, -1f, 15f, 0, true)]
	public float b;

	[DebugGUIGraph(0f, 0f, 1f, -1f, 15f, 0, true)]
	public float c;

	[DebugGUIGraph(1f, 1f, 1f, -1f, 15f, 0, true)]
	public float d;

	private void Awake()
	{
		instance = this;
		DebugGUI.SetGraphProperties("a", "a", -1f, 1f, 3, new Color(0f, 1f, 0f), autoScale: true);
		DebugGUI.SetGraphProperties("b", "b", -1f, 1f, 3, new Color(1f, 0f, 0f), autoScale: true);
		DebugGUI.SetGraphProperties("c", "c", -1f, 1f, 3, new Color(0f, 0f, 1f), autoScale: true);
		DebugGUI.SetGraphProperties("d", "d", -1f, 1f, 3, new Color(1f, 1f, 1f), autoScale: true);
	}
}
