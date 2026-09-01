using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
	public RigidbodyWithMultiplier[] rigs;

	public float offset;

	public float scale;

	public float slitherForce;

	public float upForce;

	private DataHandler data;

	private float randomOffset;

	private GeneralInput input;

	private void Start()
	{
		data = GetComponent<DataHandler>();
		randomOffset = Random.Range(0f, 100f);
		input = GetComponent<GeneralInput>();
		scale *= Random.Range(0.9f, 1.1f);
	}

	private void FixedUpdate()
	{
		if (data.isGrounded && input.inputDirection.z != 0f && !data.Dead)
		{
			for (int i = 0; i < rigs.Length; i++)
			{
				RigidbodyWithMultiplier rigidbodyWithMultiplier = rigs[i];
				rigidbodyWithMultiplier.rig.AddForce(Mathf.Cos((Time.time + randomOffset) * scale + offset * (float)i) * rigidbodyWithMultiplier.multiplier * slitherForce * data.characterForwardObject.right, ForceMode.Acceleration);
				rigidbodyWithMultiplier.rig.AddForce(rigidbodyWithMultiplier.multiplier * upForce * Vector3.up, ForceMode.Acceleration);
			}
		}
	}
}
