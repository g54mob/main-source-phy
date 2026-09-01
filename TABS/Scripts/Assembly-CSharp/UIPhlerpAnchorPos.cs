using UnityEngine;

public class UIPhlerpAnchorPos : MonoBehaviour
{
	public float spring;

	public float dampner;

	private Vector2 velocity;

	private Vector2 targetAnchoredPos;

	private RectTransform RectTransform => base.transform as RectTransform;

	public void SetNewPos(Vector2 newAnchoredPos)
	{
		targetAnchoredPos = newAnchoredPos;
	}

	private void Update()
	{
		Vector2 vector = targetAnchoredPos - RectTransform.anchoredPosition;
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.1f);
		velocity += vector * spring * num;
		velocity -= velocity * dampner * num;
		RectTransform.anchoredPosition += velocity;
	}
}
