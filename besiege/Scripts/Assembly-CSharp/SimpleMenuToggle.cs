using UnityEngine;

public class SimpleMenuToggle : ClickBehaviour, ICanBeReset
{
	public ToggleExtraOption toggleExtraOption;

	public SimpleMenuDropDown[] simpleMenuDropDowns;

	public SimpleMenuSlider[] simpleMenuSliders;

	public MeshRenderer redBackgroundMeshRenderer;

	public TextMesh textMesh;

	private bool hasStarted;

	private void Start()
	{
		hasStarted = true;
		SetGraphics();
	}

	private void OnEnable()
	{
		if (hasStarted)
		{
			SetGraphics();
		}
	}

	public override void OnClicked()
	{
		toggleExtraOption.Toggle();
		SetGraphics();
	}

	private void SetGraphics()
	{
		redBackgroundMeshRenderer.enabled = (bool)toggleExtraOption.parsedValue;
		textMesh.text = toggleExtraOption.argumentNames[0];
	}

	private void Update()
	{
		SetGraphics();
	}

	public void Reset()
	{
		toggleExtraOption.Reset();
		SetGraphics();
	}
}
