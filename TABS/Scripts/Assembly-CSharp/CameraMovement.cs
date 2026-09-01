using Landfall.TABS;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Vector3 Velocity;

	private Camera cam;

	private float m_curentDampning = 1f;

	private Vector3 m_mapCenter;

	private float m_mapRadius;

	private float m_mapRadiusSqr;

	private float mapRadiusYMultiplier;

	public float multiplier = 1f;

	private bool isSmooth;

	public float TargetDamper { get; set; }

	private void Awake()
	{
		cam = GetComponentInChildren<Camera>();
		TargetDamper = 4f;
	}

	private void Start()
	{
		m_mapRadius = MapSettings.Instance.m_mapRadius;
		mapRadiusYMultiplier = 1f / MapSettings.Instance.mapRadiusYMultiplier;
		m_mapRadiusSqr = m_mapRadius * m_mapRadius;
		m_mapCenter = MapSettings.Instance.m_mapCenter;
	}

	private void Update()
	{
		Vector3 vector = m_mapCenter - base.transform.position;
		vector.y *= mapRadiusYMultiplier;
		if (vector.sqrMagnitude > m_mapRadiusSqr)
		{
			Velocity += vector.normalized * (vector.magnitude - m_mapRadius);
		}
		m_curentDampning = Mathf.Lerp(m_curentDampning, TargetDamper, Time.unscaledDeltaTime * 10f);
		float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
		Velocity -= num * m_curentDampning * Velocity;
		Velocity = Vector3.ClampMagnitude(Velocity, 500f);
		if (float.IsNaN(Velocity.x) || float.IsNaN(Velocity.y) || float.IsNaN(Velocity.z))
		{
			Velocity = Vector3.zero;
		}
		base.transform.position += num * multiplier * Velocity;
	}

	public void ApplyVelocity(Vector3 force)
	{
		Velocity += force * Time.unscaledDeltaTime;
	}

	public void ApplyVelocityRaw(Vector3 force)
	{
		Velocity += force;
	}
}
