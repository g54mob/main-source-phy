using System.Linq;
using UnityEngine;

public class Wind : MonoBehaviour
{
	public float value;

	public float value2;

	public float value3;

	public float frequency;

	public float strength;

	public float startTime;

	[SerializeField]
	private LayerMask m_affectedLayers = -201;

	private float actualStrength;

	private Rigidbody[] rigs;

	private Vector3 wind;

	private void Start()
	{
		rigs = Object.FindObjectsOfType<Rigidbody>();
		rigs = rigs.Where((Rigidbody r) => (int)m_affectedLayers == ((int)m_affectedLayers | (1 << r.gameObject.layer))).ToArray();
		if (startTime == 0f)
		{
			actualStrength = strength;
		}
	}

	private void Update()
	{
		float num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
		if (actualStrength < strength)
		{
			actualStrength += strength * num / startTime;
		}
		Rigidbody[] array = rigs;
		foreach (Rigidbody rigidbody in array)
		{
			value = (Mathf.PerlinNoise(Time.time * frequency, Time.time * frequency + rigidbody.position.x * 0.05f) - 0.4f) * actualStrength;
			value2 = (Mathf.PerlinNoise(Time.time * frequency * 1.1f, Time.time * frequency * 1.1f + rigidbody.position.y * 0.05f) - 0.4f) * actualStrength * 0.1f;
			value3 = (Mathf.PerlinNoise(Time.time * frequency * 0.9f, Time.time * frequency * 0.9f + rigidbody.position.z * 0.05f) - 0.4f) * actualStrength;
			wind = new Vector3(value, value2, value3);
			float num2 = 1f;
			if (rigidbody.transform.name.Contains("Barrel"))
			{
				num2 = 15f;
			}
			if (rigidbody.mass > 0.1f)
			{
				rigidbody.AddForce(num * num2 * Mathf.Clamp(rigidbody.drag, 0.5f, 10f) * wind, ForceMode.Acceleration);
			}
		}
	}
}
