using UnityEngine;

public class FadeLerper : MonoBehaviour
{
	public Color offColor;

	public Color onColor;

	public float fadeValue;

	public float speed = 5f;

	private SpriteRenderer sprite;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Color b = Color.Lerp(offColor, onColor, fadeValue);
		sprite.color = Color.Lerp(sprite.color, b, Time.unscaledDeltaTime * speed);
	}
}
