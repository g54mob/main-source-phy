using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorColorCatagoryCell : MonoBehaviour
{
	public TextMeshProUGUI text;

	public Image[] colorShards;

	public Image dark;

	public RectTransform textPivot;

	private ScaleJiggle scaleJiggle;

	private bool isBeingHovered;

	public void Setup(UnitEditorColorPalette.ColorPaletteCatagory catagory, float angle)
	{
		scaleJiggle = GetComponent<ScaleJiggle>();
		textPivot.localRotation = Quaternion.Euler(0f, 0f, angle / -2f);
	}

	public void Setup(UnitEditorColorPalette.ParentCatagories parentCatagory, float angle)
	{
		scaleJiggle = GetComponent<ScaleJiggle>();
		textPivot.localRotation = Quaternion.Euler(0f, 0f, angle / -2f);
	}

	public void OnEnter()
	{
		isBeingHovered = true;
		scaleJiggle.targetScale = 1.1f;
		text.enabled = true;
	}

	public void OnExit()
	{
		isBeingHovered = false;
		scaleJiggle.targetScale = 1f;
		text.enabled = false;
	}

	private void Update()
	{
		float a = dark.color.a;
		float b = 0f;
		if (isBeingHovered)
		{
			b = 0.5f;
		}
		a = Mathf.Lerp(a, b, Time.deltaTime * 15f);
		Color color = dark.color;
		color.a = a;
		dark.color = color;
	}
}
