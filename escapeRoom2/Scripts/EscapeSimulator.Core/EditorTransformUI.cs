using UnityEngine;
using UnityEngine.UI;

public class EditorTransformUI : PineUIComponent
{
	public Canvas root;

	public RectTransform GizmoUI;

	public Image SpaceBackground;

	public Button Space;

	public Image Space_Global;

	public Image Space_Local;

	public Image ToolsBackground;

	public Toggle Move;

	public Toggle Rotate;

	public Toggle Scale;

	public RectTransform GizmoUIHide;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			PineUI.addButtonListeners(Space);
			PineUI.addToggleListeners(Move);
			PineUI.addToggleListeners(Rotate);
			PineUI.addToggleListeners(Scale);
		}
	}
}
