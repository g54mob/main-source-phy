using UnityEngine;

public class RectPositionShake : MonoBehaviour
{
	public bool isMain;

	public float multiplier = 1f;

	private Vector2 velocity;

	public float drag = 1f;

	public float spring = 1f;

	public Vector2 startLocal;

	private Vector2 startStartLocal;

	private RectTransform rectTransform;

	public Vector2 setPos;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		startLocal = rectTransform.anchoredPosition;
		startStartLocal = startLocal;
	}

	private void Start()
	{
	}

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.05f);
		velocity += (startLocal - rectTransform.anchoredPosition) * num * 50f * spring;
		velocity -= drag * velocity * 20f * num;
		rectTransform.anchoredPosition += velocity * 10f * num;
	}

	public void AddForce(Vector2 force)
	{
		velocity += force * multiplier * 10f;
	}

	public void SetPosition(Vector2 pos)
	{
		startLocal = startStartLocal + pos;
	}

	public void SetPosition()
	{
		SetPosition(setPos);
	}

	public void ResetPosision()
	{
		SetPosition(Vector2.zero);
	}
}
