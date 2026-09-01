using UnityEngine;
using UnityEngine.UI;

public class FillFollowSlider : MonoBehaviour
{
	private Image fill;

	private Slider target;

	public float drag;

	public float spring;

	private float velocity;

	public bool hard;

	private void Start()
	{
		fill = GetComponent<Image>();
		target = base.transform.parent.GetComponentInChildren<Slider>();
	}

	private void LateUpdate()
	{
		if (hard)
		{
			fill.fillAmount = target.value;
			return;
		}
		velocity += (target.value - fill.fillAmount) * spring * Time.deltaTime;
		velocity -= velocity * Time.deltaTime * drag;
		fill.fillAmount += velocity * Time.deltaTime;
		if ((fill.fillAmount == 1f || fill.fillAmount == 0f) && Mathf.Abs(velocity) > 1f)
		{
			velocity *= -0.6f;
		}
	}
}
