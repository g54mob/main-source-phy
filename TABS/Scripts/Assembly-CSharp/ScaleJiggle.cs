using UnityEngine;

public class ScaleJiggle : MonoBehaviour
{
	[SerializeField]
	private float startScale = 1f;

	[HideInInspector]
	public float extraScale;

	private float defaultScale = 1f;

	public float targetScale = 1f;

	public float spring = 1f;

	public float drag = 1f;

	[HideInInspector]
	public float velocity;

	private float cappeDeltaTime;

	protected void Start()
	{
		defaultScale = base.transform.localScale.x;
		base.transform.localScale *= startScale;
	}

	public void Update()
	{
		cappeDeltaTime = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.0166f);
		velocity += (targetScale + extraScale - base.transform.localScale.x) * spring * cappeDeltaTime * 1000f;
		velocity -= velocity * cappeDeltaTime * 20f * drag;
		base.transform.localScale += Vector3.one * velocity * cappeDeltaTime;
	}

	public void AddForce(float force)
	{
		velocity += force;
	}

	public void Reset()
	{
		velocity *= 0f;
		base.transform.localScale = Vector3.one * defaultScale;
		targetScale = defaultScale;
	}

	private void OnEnable()
	{
		Reset();
	}
}
