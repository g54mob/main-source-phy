using UnityEngine;

public class PositionShake : MonoBehaviour
{
	public bool isMain;

	public static PositionShake instance;

	public float multiplier = 1f;

	private Vector3 velocity;

	public float drag = 1f;

	public float spring = 1f;

	[HideInInspector]
	public Vector3 startLocal;

	private Vector3 startStartLocal;

	public Vector3 setPos;

	private void Awake()
	{
		if (isMain)
		{
			instance = this;
		}
		startLocal = base.transform.localPosition;
		startStartLocal = startLocal;
	}

	private void Start()
	{
	}

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.05f);
		velocity += (base.transform.parent.TransformPoint(startLocal) - base.transform.position) * num * 50f * spring;
		velocity -= drag * velocity * 20f * num;
		base.transform.position += velocity * 10f * num;
	}

	public void AddForce(Vector3 force)
	{
		velocity += force * multiplier * 10f;
	}

	public void SetPosition(Vector3 pos)
	{
		startLocal = startStartLocal + pos;
	}

	public void SetPosition()
	{
		SetPosition(setPos);
	}

	public void ResetPosision()
	{
		SetPosition(startStartLocal);
	}
}
