using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleCreatorPageToggle : MonoBehaviour
{
	[SerializeField]
	private float onToggleScale;

	[SerializeField]
	private Color onToggleColor;

	[SerializeField]
	private float offToggleScale;

	[SerializeField]
	private Color offToggleColor;

	private Toggle toggle;

	private TextMeshProUGUI textMesh;

	private ScaleJiggle scaleJiggle;

	private void Awake()
	{
		toggle = GetComponent<Toggle>();
		scaleJiggle = GetComponent<ScaleJiggle>();
		textMesh = GetComponent<TextMeshProUGUI>();
		toggle.onValueChanged.AddListener(OnToggle);
		OnToggle(toggle.isOn);
	}

	private void OnToggle(bool isOn)
	{
		if (isOn)
		{
			textMesh.color = onToggleColor;
			scaleJiggle.targetScale = onToggleScale;
		}
		else
		{
			textMesh.color = offToggleColor;
			scaleJiggle.targetScale = offToggleScale;
		}
	}
}
