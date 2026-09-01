using UnityEngine;

public class Water : MonoBehaviour
{
	public float force = 1000f;

	public float deadForce;

	public float streamForce;

	public float waterSinkOffset = -800f;

	public float damageOverTime;

	private LayerMask m_killMask;

	private void Start()
	{
		m_killMask = LayerMask.NameToLayer("MainRig");
		Collider component = GetComponent<Collider>();
		if ((bool)WaterPost.instance && (bool)component)
		{
			WaterPost.instance.AddWaterCollider(component);
		}
		damageOverTime *= 10f;
	}

	private void OnDisable()
	{
		Collider component = GetComponent<Collider>();
		if ((bool)WaterPost.instance && (bool)component)
		{
			WaterPost.instance.AddWaterCollider(component);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		Rigidbody componentInParent = other.GetComponentInParent<Rigidbody>();
		if (!componentInParent)
		{
			return;
		}
		DataHandler componentInChildren = other.transform.root.GetComponentInChildren<DataHandler>();
		if ((bool)componentInChildren)
		{
			float num = Mathf.Clamp((base.transform.position.y + waterSinkOffset - other.transform.position.y) * 10f, 0f, 10f) * Time.deltaTime;
			if (damageOverTime != 0f && !componentInChildren.Dead && other.gameObject.layer == (int)m_killMask)
			{
				componentInChildren.healthHandler.TakeDamage(damageOverTime * Time.deltaTime * (0.01f * componentInChildren.maxHealth), Vector3.zero, null);
			}
			if (componentInChildren.Dead && deadForce != 0f)
			{
				componentInParent.AddForce(deadForce * num * Vector3.up, ForceMode.Acceleration);
			}
			componentInChildren.sinceGrounded = Mathf.Clamp(componentInChildren.sinceGrounded, float.NegativeInfinity, 1f);
			componentInParent.AddForce(force * num * Vector3.up, ForceMode.Acceleration);
			componentInParent.AddForce(streamForce * num * base.transform.forward, ForceMode.Acceleration);
		}
		_ = componentInParent.mass;
		_ = 100f;
		componentInParent.angularVelocity *= 0.15f;
		componentInParent.velocity *= 0.15f;
	}
}
