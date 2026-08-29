using UnityEngine;
using UnityEngine.UI;

public class VirtualCursorUI : PineUIComponent
{
	public Canvas root;

	public Image Icon;

	public CanvasGroup MovementSpeed;

	public Text MovementSpeed_Text;

	public RawImage MaterialDragIcon;

	public object data;

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
		}
	}
}
