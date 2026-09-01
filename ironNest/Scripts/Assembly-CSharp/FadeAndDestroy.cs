using UnityEngine;
using UnityEngine.UI;

public class FadeAndDestroy : MonoBehaviour
{
	public AnimationCurve opacityCurve;

	public float destroyAfterSeconds;

	private Image img;

	private float timer;

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
