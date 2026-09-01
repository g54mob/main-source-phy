using NaughtyAttributes;
using UnityEngine;

public class Sliders : MonoBehaviour
{
	[BoxGroup("Sliders")]
	[Slider(0, 10)]
	public int intSlider;

	[BoxGroup("Sliders")]
	[Slider(0f, 10f)]
	public float floatSlider;

	[BoxGroup("Sliders")]
	[MinMaxSlider(0f, 100f)]
	public Vector2 minMaxSlider;
}
