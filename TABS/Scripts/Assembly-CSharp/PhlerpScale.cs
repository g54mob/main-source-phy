using UnityEngine;

public class PhlerpScale : MonoBehaviour
{
	public bool useUnscaledTime;

	public Vector3 targetScale = Vector3.one;

	public float drag;

	public float spring;

	public float m_StartScale = 0.7f;

	private Vector3 velocity;

	private void Start()
	{
		base.transform.localScale = Vector3.one * m_StartScale;
	}

	private void Update()
	{
		float num = (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
		velocity += (targetScale - base.transform.localScale) * (num * spring);
		velocity -= velocity * (num * drag);
		base.transform.localScale += velocity * num;
	}

	public void AddForce(float force)
	{
		velocity += force * Vector3.one;
	}
}
