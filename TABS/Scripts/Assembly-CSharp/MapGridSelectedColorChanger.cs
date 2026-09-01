using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGridSelectedColorChanger : MonoBehaviour
{
	public Color newTextColor = Color.black;

	public Color newGradientColor = Color.white;

	public Image gradient;

	public TextMeshProUGUI text;

	public CurveScaler curveScaler;

	public void Selected()
	{
		gradient.color = newGradientColor;
		text.color = newTextColor;
		curveScaler.enabled = true;
	}
}
